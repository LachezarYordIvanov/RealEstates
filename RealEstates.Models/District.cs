using System.Collections.Generic;

namespace RealEstates.Models
{
    public class District
    {
        public District()
        {
            this.Properties = new HashSet<RealEstateProperty>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<RealEstateProperty> Properties { get; set; }
    }
}
