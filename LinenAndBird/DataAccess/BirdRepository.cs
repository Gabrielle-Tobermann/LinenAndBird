
using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LinenAndBird.DataAccess
{
    public class BirdRepository
    {
        static List<Bird> _birds = new List<Bird>
        {
            new Bird
            {
                Id = Guid.NewGuid(),
                Name = "Jimmy",
                Color = "Red",
                Size = "Small",
                Type = BirdType.Dead,
                Accessories = new List<string> {"Beannie", "Gold Wing Tips"}
            }
        };

        internal IEnumerable<Bird> GetAll()
        {
            // connections are like the tunnel /w apps and databases
            using var connection = new SqlConnection("Server=localhost;Database=LinenAndBird;Trusted_Connection=True;");
            // connections aren't open by default
            connection.Open();

            // this is what tells sql what we want to do
            var command = connection.CreateCommand();
            command.CommandText = @"select *
                                    From Birds";

            // execute reader is for when we care about getting all the results of our query
            var reader = command.ExecuteReader();

            var birds = new List<Bird>();

            // data readers are weird, they only get one row from the results at a time
            while (reader.Read())
            {
                // Mapping data from the relational model to the object model
                var bird = new Bird();
                bird.Size = reader["Size"].ToString();
                bird.Id = reader.GetGuid(0);
                bird.Type = (BirdType)reader["Type"];
                bird.Name = reader["Name"].ToString();
                bird.Color = reader["Color"].ToString();
                // each bird goes in the list to return later
                birds.Add(bird);
            }
            return birds;
            // return _birds;
        }
        internal void Add(Bird newBird)
        {
            newBird.Id = Guid.NewGuid();
            _birds.Add(newBird);
        }

        internal Bird GetById(Guid birdId)
        {
            // connections are like the tunnel /w apps and databases
            using var connection = new SqlConnection("Server=localhost;Database=LinenAndBird;Trusted_Connection=True;");
            // connections aren't open by default
            connection.Open();

            // this is what tells sql what we want to do
            var command = connection.CreateCommand();
            command.CommandText = $@"select *
                                    From Birds
                                    where id = @id";

            // parameterization prevents sql injections
            command.Parameters.AddWithValue("id", birdId);

            // return _birds.FirstOrDefault(hat => hat.Id == birdId);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var bird = new Bird();
                bird.Size = reader["Size"].ToString();
                bird.Id = reader.GetGuid(0);
                bird.Type = (BirdType)reader["Type"];
                bird.Name = reader["Name"].ToString();
                bird.Color = reader["Color"].ToString();

                return bird;
            }

            return null;
        }
    }
}
