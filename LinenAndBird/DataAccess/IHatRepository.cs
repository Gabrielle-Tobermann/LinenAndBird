using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.DataAccess
{
    interface IHatRepository
    {
        Hat GetById(Guid id);
        List<Hat> GetAll();
        void Add(Hat newHat);
        IEnumerable<Hat> GetHatsByStyle(HatStyle style);
    };
}
