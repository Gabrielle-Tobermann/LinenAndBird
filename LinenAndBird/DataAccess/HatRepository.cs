using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.DataAccess
{
    public class HatRepository
    {
        static List<Hat> _hats = new List<Hat>
        {
            new Hat
                {
                    Id = Guid.NewGuid(),
                    Color = "Blue",
                    Designer = "Charlie",
                    Style = HatStyle.OpenBack
                },
                new Hat
                {
                    Id = Guid.NewGuid(),
                    Color = "Magenta",
                    Designer = "Nathan",
                    Style = HatStyle.Normal
                },
                new Hat
                {
                    Id = Guid.NewGuid(),
                    Color = "Black",
                    Designer = "Charlie",
                    Style = HatStyle.WideBrim
                },
        };

        internal Hat GetById(Guid hatId)
        {
            return _hats.FirstOrDefault(hat => hat.Id == hatId);
        }

        internal List<Hat> GetAll()
        {
            return _hats;
        }

        internal void Add(Hat newHat)
        {
            _hats.Add(newHat);
        }

        internal IEnumerable<Hat> GetByStyle(HatStyle style)
        {
            return _hats.Where(hat => hat.Style == style);
        }
    }
}
