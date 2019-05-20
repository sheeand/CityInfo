using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    //--  This is our in-memory data store (in lieu of using Entity Framework Core)

    public class CitiesDataStore
    {
        /// <summary>
        /// DS - Returns an instance of CitiesDataStore and allows you to keep working 
        /// with the same ("current") set of data as long as you don't restart.
        /// </summary>
        public static CitiesDataStore Current { get; } = new CitiesDataStore(); //--  Property initializer syntax
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            // init dummy data
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The big apple",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "A big park"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Grand Central Station",
                            Description = "A big train station"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Indianapolis",
                    Description = "The capital of Indiana",
                     PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Indianapolis Motor Speedway",
                            Description = "Location of the Indy 500"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "War Memorial",
                            Description = "A nice museum"
                        }
                    }
               },
                new CityDto()
                {
                    Id = 3,
                    Name = "Chicago",
                    Description = "Largest city in Illinois",
                     PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "OHare International Airport",
                            Description = "A big airport"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "Midway Intrernational Airport",
                            Description = "Another big airport"
                        }
                    }
               }

            };
        }
    }
}
