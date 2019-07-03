using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        // This is the contract by which the repository implementation will have to adhere to

        // All services (like this one) need to be registered in the StartUp class

        // The repository is where data persistence logic is applied
        // Data persistence is provided by the database context
        // We provide an instance of CityInfoDbContext via constructor injection
        private CityInfoDbContext _context;
        public CityInfoRepository(CityInfoDbContext context)
        {
            _context = context;
        }

        // The persisted data is utilized in the subsequent methods

        // Unless the API is going to have a huge set of data-shaping queries, requiring dozens of methods,
        // use IEnumerable instead if IQueryable, because if we return an IQueryable, 
        // the consumer would be able to build on that IQueryable (e.g. by adding an OrderBy clause or Where clause, etc.),
        // possibly before the query is executed. This effectively would be leaking persistence-related logic
        // out of the repository, which violates the persistence ignorant advantage of the repository pattern.

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefault();
            }

            return _context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterest
                .Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _context.PointsOfInterest
                .Where(p => p.CityId == cityId).ToList();
        }
    }
}
