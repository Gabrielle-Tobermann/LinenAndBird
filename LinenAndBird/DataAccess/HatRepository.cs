using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;

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

        const string _connectionString = "Server=localhost;Database=LinenAndBird;Trusted_Connection=True;";


        internal Hat GetById(Guid hatId)
        {
            using var db = new SqlConnection(_connectionString);

            var hat = db.QueryFirstOrDefault<Hat>("Select * from Hats where Id = @id", new { id = hatId });

            return hat;
            // return _hats.FirstOrDefault(hat => hat.Id == hatId);
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
