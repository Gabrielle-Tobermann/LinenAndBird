
using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LinenAndBird.DataAccess
{
    public class BirdRepository
    {
        const string _connectionString = "Server=localhost;Database=LinenAndBird;Trusted_Connection=True;";

        internal IEnumerable<Bird> GetAll()
        {
            // connections are like the tunnel /w apps and databases
            using var db = new SqlConnection(_connectionString);

            var birds = db.Query<Bird>(@"Select * From Birds");

            return birds; 
            // connections aren't open by default
            //connection.Open();

            //// this is what tells sql what we want to do
            //var command = connection.CreateCommand();
            //command.CommandText = @"select *
            ////                        From Birds";

            //// execute reader is for when we care about getting all the results of our query
            //var reader = command.ExecuteReader();

            //var birds = new List<Bird>();

            // data readers are weird, they only get one row from the results at a time
            //while (reader.Read())
            //{
            //    // Mapping data from the relational model to the object model
            //    var bird = new Bird();
            //    bird.Size = reader["Size"].ToString();
            //    bird.Id = reader.GetGuid(0);
            //    bird.Type = (BirdType)reader["Type"];
            //    bird.Name = reader["Name"].ToString();
            //    bird.Color = reader["Color"].ToString();
            //    // each bird goes in the list to return later
            //    birds.Add(bird);
            //}
            //return birds;
            // return _birds;
        }

        internal void Remove(Guid id)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"Delete
                       From Birds
                       Where Id = @id";

            db.Execute(sql, new { id });
            //connection.Open();

            //var cmd = connection.CreateCommand();
            //cmd.CommandText = @"Delete
            //                    From Birds
            //                    Where Id = @id";

            //cmd.Parameters.AddWithValue("id", id);

            //cmd.ExecuteNonQuery();

        }

        internal object Update(Guid id, Bird bird)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"update Birds
                      Set Color = @color,
                          Name = @name,
                          Size = @size,
                      output inserted.*
                      Where id = @id";

            bird.Id = id;
            var updatedBird = db.QuerySingleOrDefault<Bird>(sql, bird);
            return bird;

            //connection.Open();

            //var cmd = connection.CreateCommand();
            //cmd.CommandText = @"update Birds
            //                    Set Color = @color,
            //                        Name = @name,
            //                        Size = @size,
            //                    output inserted.*
            //                    Where id = @id";

            //cmd.Parameters.AddWithValue("Type", bird.Type);
            //cmd.Parameters.AddWithValue("Color", bird.Color);
            //cmd.Parameters.AddWithValue("Size", bird.Size);
            //cmd.Parameters.AddWithValue("Name", bird.Name);
            //cmd.Parameters.AddWithValue("id", id);

            //var reader = cmd.ExecuteReader();
            //if (reader.Read())
            //{
            //    return MapFromReader(reader);
            //}

            //return null;
        }

        internal void Add(Bird newBird)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"insert into birds(Type,Color,Size,Name)
                                output inserted.Id
                                values (@Type,@Color,@Size,@Name)";

            var id = db.ExecuteScalar<Guid>(sql, newBird);
            newBird.Id = id;
            //connection.Open();

            //var cmd = connection.CreateCommand();
            //cmd.CommandText = @"insert into birds(Type,Color,Size,Name)
            //                    output inserted.Id
            //                    values (@Type,@Color,@Size,@Name)";

            //cmd.Parameters.AddWithValue("Type", newBird.Type);
            //cmd.Parameters.AddWithValue("Color", newBird.Color);
            //cmd.Parameters.AddWithValue("Size", newBird.Size);
            //cmd.Parameters.AddWithValue("Name", newBird.Name);

            //// executeNonQuery will just return the number of rows affected
            //// var numOfRowsAffected = cmd.ExecuteNonQuery();
            //var newId = (Guid) cmd.ExecuteScalar();
            //newBird.Id = newId;

            //newBird.Id = Guid.NewGuid();
            //_birds.Add(newBird);
        }

        internal Bird GetById(Guid birdId)
        {
            // connections are like the tunnel /w apps and databases
            using var db = new SqlConnection(_connectionString);
            // connections aren't open by default
            // connection.Open();

            // this is what tells sql what we want to do
            //var command = connection.CreateCommand();
            //command.CommandText = $@"select *
            //                        From Birds
            //                        where id = @id";

            //// parameterization prevents sql injections
            //command.Parameters.AddWithValue("id", birdId);

            // return _birds.FirstOrDefault(hat => hat.Id == birdId);

            //var reader = command.ExecuteReader();

            //if (reader.Read())
            //{
            //    MapFromReader(reader);
            //}

            //return null;

            var sql = $@"select *
                      From Birds
                      where id = @id";

            var bird = db.QueryFirst<Bird>(sql, new { id = birdId });

            return bird;
        }

        Bird MapFromReader(SqlDataReader reader)
        {
                // Mapping data from the relational model to the object model
                var bird = new Bird();
                bird.Size = reader["Size"].ToString();
                bird.Id = reader.GetGuid(0);
                bird.Type = (BirdType)reader["Type"];
                bird.Name = reader["Name"].ToString();
                bird.Color = reader["Color"].ToString();
                // each bird goes in the list to return later
                return bird;
        }
    }
}
