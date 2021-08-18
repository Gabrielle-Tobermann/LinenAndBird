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
        static List<Hat> _hats = new List<Hat>
        {
            new Hat
                {
                    Color = "Blue",
                    Designer = "Charlie",
                    Style = HatStyle.OpenBack
                },
                new Hat
                {
                    Color = "Magenta",
                    Designer = "Nathan",
                    Style = HatStyle.Normal
                },
                new Hat
                {
                    Color = "Black",
                    Designer = "Charlie",
                    Style = HatStyle.WideBrim
                },
        };

        [HttpGet]
        public List<Hat> GetAllHats()
        {
            return _hats;
        
        }

        // if you pass in something to httpGet, it will add it to the route
        // name inside of curly braces NEEDS to match the parameter of function below
        // this royte would be: GET /api/hats/styles/1 ==> all opened backed hats
        [HttpGet("styles/{style}")]
        public IEnumerable<Hat> GetHatsByStyle(HatStyle style)
        {
            var matches = _hats.Where(hat => hat.Style == style);
            return matches.ToList();
        }

        [HttpPost]
        public void AddAHat(Hat newHat)
        {
            _hats.Add(newHat);
        }
    }
}
