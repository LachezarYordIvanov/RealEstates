using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class DistrictsService : IDistrictsService
    {
        private readonly RealEstateDbContext db;

        public DistrictsService(RealEstateDbContext db)
        {
            this.db = db;            
        }
        public IEnumerable<DistrictViewModel> GetTopDistrictsByAveragePrice(int count = 10)
        {
            return this.db.Districts.OrderByDescending(x => x.Properties.Average(x => x.Price))
                .Select(x => MapToDistrictViewModel(x))
                .Take(count)
                .ToList();
        }
        public IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10)
        {
            return this.db.Districts
                .OrderByDescending(x => x.Properties.Count())
                .Select(x => MapToDistrictViewModel(x))
                .Take(count)
                .ToList();
        }
        private static DistrictViewModel MapToDistrictViewModel(District x)
        {
            return new DistrictViewModel
            {
                Name = x.Name,
                AveragePrice = x.Properties.Average(x => x.Price),
                MinPrice = x.Properties.Min(x => x.Price),
                MaxPrice = x.Properties.Max(x => x.Price),
                PropertiesCount = x.Properties.Count(),
            };
        }
    }
}
