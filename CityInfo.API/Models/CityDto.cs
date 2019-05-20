using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }

        // --  It's good to initialize this to an empty collection 
        // --  instead of leaving it at null as to avoid null reference issues
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } 
            = new List<PointOfInterestDto>();


    }
}
