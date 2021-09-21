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
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        BirdRepository _birdRepo;
        HatRepository _hatRepo;
        OrdersRepository _orderRepo;

        public OrdersController()
        {
            _birdRepo = new BirdRepository();
            _hatRepo = new HatRepository();
            _orderRepo = new OrdersRepository();
        }


        public IActionResult GetAllOrders()
        {
            return Ok(_orderRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetORderById(Guid id)
        {
            Order order = _orderRepo.Get(id);

            if (order == null)
                return NotFound("No order exists with that id.");

            return Ok(order);
        }

        [HttpPost("bird/{birdID}/hat/{hatId}/{price}")]
        public IActionResult CreateOrder(CreateOrderCommand command)
        {
            var hatToOrder = _hatRepo.GetById(command.HatId);
            var birdToOrder = _birdRepo.GetById(command.BirdId);
            if (hatToOrder == null)
            {
                return NotFound("There was no matching hat in the database.");
            }
            if (birdToOrder == null)
            {
                return NotFound("There was no matching bird in the database.");
            }

            var order = new Order
            {
                Bird = birdToOrder,
                Hat = hatToOrder,
                Price = command.Price
            };

            _orderRepo.Add(order);
            return Created($"/api/orders/{order.Id}", order);
        }
    }
}
