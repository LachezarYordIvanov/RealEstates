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
            return db.RealEstateProperties
                .Where(x => x.Year >= minYear && x.Year <= maxYear && x.Size >= minSize && x.Size <= maxSize)
                .Select(x => MapToPropertyViewModel(x))
                .OrderBy(x => x.Price)
                .ToList();
        }
        
        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            return this.db.RealEstateProperties
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                .Select(x => MapToPropertyViewModel(x))
                .OrderBy(x => x.Price)
                .ToList();
        }

        public void UpdateTags(int propertyId)
        {
            var property = this.db.RealEstateProperties.FirstOrDefault(x => x.Id == propertyId);
            property.Tags.Clear();

            if (property.Year.HasValue && property.Year < 1900)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("OldBuilding")
                    });
            }

            if (property.Size > 120)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("HugeApartment")
                    });
            }

            if (property.Year > 2018 && property.TotalNumberOfFloors > 5)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("HasParking")
                    });
            }

            if (property.Floor == property.TotalNumberOfFloors)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("LastFloor")
                    });
            }

            if (((double)property.Price / property.Size) < 800)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("CheapProperty")
                    });
            }

            if (((double)property.Price / property.Size) > 2000)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("ExpensiveProperty")
                    });
            }

            this.db.SaveChanges();
        }

        private Tag GetOrCreateTag(string tag)
        {
            var tagEntity = this.db.Tags.FirstOrDefault(x => x.Name.Trim() == tag.Trim());

            if (tagEntity == null)
            {
                tagEntity = new Tag { Name = tag };
            }

            return tagEntity;
        }

        private static PropertyViewModel MapToPropertyViewModel(RealEstateProperty x)
        {
            return new PropertyViewModel
            {
                Price = x.Price,
                Floor = (x.Floor ?? 0) + "/" + (x.TotalNumberOfFloors ?? 0),
                Size = x.Size,
                Year = x.Year,
                BuildingType = x.BuildingType.Name,
                District = x.District.Name,
                PropertyType = x.PropertyType.Name
            };
        }

    }
}
