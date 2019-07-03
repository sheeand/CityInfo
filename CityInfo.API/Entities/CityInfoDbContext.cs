using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class CityInfoDbContext : DbContext
    {
        // There's no need for all the entities that map to tables in a database to be in the same context.
        // Multiple contexts can work on the same database.

        public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options) : base(options)
        {

            // To use migrations to create and update the database:
            // 1.) Make sure the entities in the Entities folder are the way you want them
            // 2.) In Package Manager Console, type Add-Migration [an arbitrary name for this migration]
            //          This command adds the Migrations folder and generates two files: 
            //              a.) a snapshot of the context model (all the entities in the Entities folder)
            //              b.) code used by the migration builder to update or roll back the database
            // 3.) Running the application with Database.Migrate() applies the migration files to the database

            Database.Migrate();
        }

        // Don't forget to register these in the Startup.cs file
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        // We can configure the database connection, or in the DbContext Definition (recommended)

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
