using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

// ctrl shift m pops open the window


namespace LinenAndBird.DataAccess
{
    public class OrdersRepository
    {
        readonly string _connectionString;

        public OrdersRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LinenAndBird");
        }
        internal IEnumerable<Order> GetAll()
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"select *
                        from Orders o
	                        join Birds b 
		                        on b.Id = o.BirdId
	                        join Hats h
	                        on h.Id = o.HatId";

            var results = db.Query<Order, Bird, Hat, Order>(sql, (order, bird, hat) =>
            {
                order.Bird = bird;
                order.Hat = hat;
                return order;
            }, splitOn: "Id");
            // spliton: id means as soon as it's gonna get to the Id column, it moves to the next thing --> hat --> bird

            return results;
        }

        internal Order Get(Guid id)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"select *
                        from Orders o
	                        join Birds b 
		                        on b.Id = o.BirdId
	                        join Hats h
	                        on h.Id = o.HatId
                        where o.id = @id";

            // multimapping doesn't work for any other kind of dapper call,
            // so we vtake the collection and turn it into one item ourselves
            var orders = db.Query<Order, Bird, Hat, Order>(sql, (order, bird, hat) =>
            {
                order.Bird = bird;
                order.Hat = hat;
                return order;
            },
            new { id },
            splitOn: "Id");

            return orders.FirstOrDefault();

        }

        //static List<Order> _orders = new List<Order>();
        internal void Add(Order order)
        {
            // using kw will dispose of the connection when reaching the closing curly brace for that function
            using var db = new SqlConnection(_connectionString);

            var sql = @"INSERT INTO [dbo].[Orders]
                                ([BirdId]
                                ,[HatId]
                                ,[Price])
                        Output inserted.Id
                        VALUES 
                                (@BirdId
                                ,@HatId
                                '@Price)";
            var parameters = new {
                BirdId = order.Bird.Id,
                HatId = order.Hat.Id,
                Price = order.Price
            };
            var id = db.ExecuteScalar<Guid>(sql, parameters);


            //order.Id = Guid.NewGuid();
            //_orders.Add(order);
        }
    }
}
