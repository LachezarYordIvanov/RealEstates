using RealEstates.Services.Models;
using System.Collections.Generic;

namespace RealEstates.Services
{
    public class DistrictsService : IDistrictsService
    {

        public IEnumerable<DistrictViewModel> GetTopDistrictsByAveragePrice(int count = 10)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10)
        {
            throw new System.NotImplementedException();
        }
    }
}
