﻿using LinenAndBird.DataAccess;
using LinenAndBird.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LinenAndBird.Controllers
{
    [Route("api/birds")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        BirdRepository _repo;
        // this is asking asp.net for the application configuration
        // this is known as Dependency Injection
        public BirdController(BirdRepository repo)
        {
            // var connectionString = config.GetConnectionString("LinenAndBird");
            // _repo = new BirdRepository(connectionString);
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAllBirds ()
        {
            return  Ok(_repo.GetAll());
        }

        [HttpPost]
        public IActionResult AddBird(Bird newBird)
        {
            if (string.IsNullOrEmpty(newBird.Name) || string.IsNullOrEmpty(newBird.Color))
            {
                return BadRequest("Name and Color are required fields");
            }
            _repo.Add(newBird);

            return Created($"/api/birds/{newBird.Id}", newBird);
        }

        [HttpGet("{id}")]
        public IActionResult GetBirdById(Guid id)
        {
            var bird = _repo.GetById(id);

            if (bird == null)
            {
                return NotFound($"No bird with the id {id} was found.");
            }

            return Ok(bird);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBird(Guid id)
        {
            _repo.Remove(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBird(Guid id, Bird bird)
        {
            var birdToUpdate = _repo.GetById(id);

            if (birdToUpdate == null)
            {
                return NotFound($"Could not find bird with the id {id} for updating");
            }

            var updateBird = _repo.Update(id, bird);

            return Ok(updateBird);
        }
    }
}
