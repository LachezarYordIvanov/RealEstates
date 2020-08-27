using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class PropertiesService : IPropertiesService
    {
        private RealEstateDbContext db; 
        public PropertiesService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public void Create(string district, int size, int? year, int price, string propertyType, string buildingType, int? floor, int? maxFloors)
        {
            var property = new RealEstateProperty
            {
                Size = size,
                Price = price,
                Year = year,
                Floor = floor,
                TotalNumberOfFloors = maxFloors
            };

            if (property.Year < 1800)
            {
                property.Year = null;
            }

            if (property.Floor <= 0)
            {
                property.Floor = null;
            }

            if (property.TotalNumberOfFloors <= 0)
            {
                property.TotalNumberOfFloors = null; 
            }

            //District
            var districtEntity = this.db.Districts
                .FirstOrDefault(x => x.Name.Trim() == district.Trim());

            if (districtEntity == null)
            {
                districtEntity = new District
                {
                    Name = district
                };
            }

            property.District = districtEntity;

            //Building Type
            var buildingTypeEntity = this.db.BuildingTypes
                .FirstOrDefault(x => x.Name.Trim() == buildingType.Trim());

            if (buildingTypeEntity == null)
            {
                buildingTypeEntity = new BuildingType
                {
                    Name = buildingType
                };
            }

            property.BuildingType = buildingTypeEntity;

            //Property Type

            var propertyTypeEntity = this.db.PropertyTypes
                .FirstOrDefault(x => x.Name.Trim() == propertyType.Trim());

            if (propertyTypeEntity == null)
            {
                propertyTypeEntity = new PropertyType
                {
                    Name = propertyType
                };
            }

            property.PropertyType = propertyTypeEntity;

            this.db.RealEstateProperties.Add(property);
            this.db.SaveChanges();

            this.UpdateTags(property.Id);
        }

        
        public IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTags(int propertyId)
        {
            throw new System.NotImplementedException();
        }
    }
}
