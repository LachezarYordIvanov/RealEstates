using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services;
using System;
using System.Text;

namespace RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var db = new RealEstateDbContext();

            db.Database.Migrate();

            var propertiesServise = new PropertiesService(db);

            IDistrictsService districtsService = new DistrictsService(db);
            var districts = districtsService.GetTopDistrictsByAveragePrice();

            foreach (var district in districts)
            {
                Console.WriteLine($"{district.Name} => Price:{district.AveragePrice:0.00} ({district.MinPrice}-{district.MinPrice}) => {district.PropertiesCount} properties");
            }
        }
    }
}
