using Application.Cities.Commands;
using Application.Cities.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CitiesController : ApiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<CityDto>>> Get()
        {
            return await Mediator.Send(new GetCitiesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            return await Mediator.Send(new GetCityQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCityCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCityCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Update(int id)
        {

            await Mediator.Send(new DeleteCityCommand { Id = id });

            return Ok();
        }
    }
}
