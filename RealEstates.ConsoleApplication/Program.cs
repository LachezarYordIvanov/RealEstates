using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services;

namespace RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new RealEstateDbContext();

            db.Database.Migrate();

            var propertiesServise = new PropertiesService(db);
            

        }
    }
}
