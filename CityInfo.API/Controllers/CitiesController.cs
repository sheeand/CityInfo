using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityInfo.API.Controllers
{
    [Route("api")]
    public class CitiesController : Controller
    {
        [HttpGet("json/cities")]
        public JsonResult GetCitiesJson()
        {
            //--  JsonResult does not support helper methods for returning specific things
            //--  The return should be that which the API consumer expects to receive

            //return new JsonResult(new List<object>()
            //{
            //    new {id=1, Name="New York City"},
            //    new {id=2, Name="Albany"}
            //});

            var result = new JsonResult(CitiesDataStore.Current.Cities);
            result.StatusCode = 418;  //--  I'm a teapot
            return result;
        }

        [HttpGet("json/cities/{id}")]
        public JsonResult GetCityJson(int id)
        {
            return new JsonResult(

                CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id)
                );
        }

        [HttpGet("iaction/cities")]
        public IActionResult GetCitiesAction()
        {
            //return new JsonResult(new List<object>()
            //{
            //    new {id=1, Name="New York City"},
            //    new {id=2, Name="Albany"}
            //});

            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("iaction/cities/{id}")]
        public IActionResult GetCityAction(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);
        }
    }
}
