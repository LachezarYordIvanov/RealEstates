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

            IPropertiesService propertiesService = new PropertiesService(db);

            Console.Write("Min. Price: ");
            int minPrice = int.Parse(Console.ReadLine());
            Console.Write("Max. Price: ");
            int maxPrice = int.Parse(Console.ReadLine());

            var properties = propertiesService.SearchByPrice(minPrice, maxPrice);
            foreach (var property in properties)
            {
                Console.WriteLine($"{property.District}, fl.{property.Floor}, {property.Size} m², {property.Year}, {property.Price} €, {property.PropertyType}, {property.BuildingType}");
            }

            //var propertiesServise = new PropertiesService(db);
            //IDistrictsService districtsService = new DistrictsService(db);
            //var districts = districtsService.GetTopDistrictsByAveragePrice();
            //foreach (var district in districts)
            //{
            //    Console.WriteLine($"{district.Name} => Price:{district.AveragePrice:0.00} ({district.MinPrice}-{district.MinPrice}) => {district.PropertiesCount} properties");
            //}
        }
    }
}
