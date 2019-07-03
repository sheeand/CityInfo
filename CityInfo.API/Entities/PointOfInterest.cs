using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        // Dto's do not have properties and annotations as these entity classes have

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        // By convention, a relationship will be created when there is a navigation property
        // discovered on a type. 
        // A property is considered a navigation property if the type 
        // it points to cannot be mapped as a scalar type by the current database provider. 
        // So a property City of type City will be considered a navigation property, 
        public City City { get; set; }

        // and its foreign key can either be specified by annotation,
        // [ForeignKey("CityId")]

        // or implied by convention with "Id"
        public int CityId { get; set; }
    }
}
