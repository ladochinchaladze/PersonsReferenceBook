using Application.Common.Paging;
using Application.Persons.Commands;
using Application.Persons.Commands.Create;
using Application.Persons.Commands.Delete;
using Application.Persons.Commands.Image;
using Application.Persons.Commands.Update;
using Application.Persons.Queries;
using Application.Persons.Queries.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class PersonsController : ApiControllerBase
    {

        [HttpGet]
        public async Task<List<PersonsDto>> Get()
        {
            return await Mediator.Send(new GetPersonsQuery());
        }


        [HttpGet("{id}")]
        public async Task<PersonsDto> Get(int id)
        {
            return await Mediator.Send(new GetPersonQuery { Id = id });
        }


        [HttpPost]
        public async Task<ActionResult> Create(CreatePersonCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdatePersonCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePersonCommand { Id = id });

            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<string>> UpdateImage(UpdateImageCommand command)
        {
            var imageName = await Mediator.Send(command);

            return imageName;
        }

        [HttpGet("{personId}")]
        public async Task<ActionResult> GetImage(int personId)
        {
            var imagePath = await Mediator.Send(new GetImagePathQuery { PersonId = personId });

            return PhysicalFile(imagePath, "image/jpg");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<PagedResult<PersonsDto>>> FastSearch(
            [FromQuery] FilterBaseQuery<PersonFastSearchFilter, PagedResult<PersonsDto>> query)
        {
            return await Mediator.Send(query);
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<PagedResult<PersonsDto>>> FullSearch(
            [FromQuery] FilterBaseQuery<PersonFullSearchFilter, PagedResult<PersonsDto>> query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<List<ReportVM>> Report()
        {
            return await Mediator.Send(new GetReportQuery());
        }

    }
}
