using LinenAndBird.DataAccess;
using LinenAndBird.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.Controllers
{
    // attributes
    [Route("api/hats")] // exposed at this enpoint
    [ApiController] // API controller so returns JSON or XML
    public class HatsController : ControllerBase
    {
        HatRepository _repo;
       public HatsController()
        {
            _repo = new HatRepository();
        }

        [HttpGet]
        public List<Hat> GetAllHats()
        {
            return _repo.GetAll();
        
        }

        // if you pass in something to httpGet, it will add it to the route
        // name inside of curly braces NEEDS to match the parameter of function below
        // this royte would be: GET /api/hats/styles/1 ==> all opened backed hats
        [HttpGet("styles/{style}")]
        public IEnumerable<Hat> GetHatsByStyle(HatStyle style)
        {
            return _repo.GetByStyle(style);
        }

        [HttpPost]
        public void AddAHat(Hat newHat)
        {
            _repo.Add(newHat);
        }
    }
}
