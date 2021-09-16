using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entiities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons.Commands.Update
{
    public class UpdatePersonCommand : IRequest<Unit>, IPersonCommandBase
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public List<ContactModel> Contacts { get; set; }
        public List<ReletivePersonsModel> ReletivePersons { get; set; }



        public class UpdatePersonCommandhandler : IRequestHandler<UpdatePersonCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IImageService _fileService;

            public UpdatePersonCommandhandler(IApplicationDbContext context, IImageService fileService)
            {
                _context = context;
                _fileService = fileService;
            }

            public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
            {

                var person = await _context
                    .Persons
                    .Include(x => x.Contacts)
                    .Include(x => x.PersonRelatedPersons)
                    .FirstAsync(x => x.Id == request.Id, cancellationToken);



                person.FirstName = request.FirstName;
                person.LastName = request.LastName;
                person.Gender = request.Gender;
                person.IdentityNumber = request.IdentityNumber;
                person.BirthDate = request.BirthDate;
                person.CityId = request.CityId;


                person.Contacts = request.Contacts.Select(x => new Domain.Entiities.Contact
                {
                    Number = x.Number,
                    ContactType = x.ContactType
                }).ToList();

                person.PersonRelatedPersons = request.ReletivePersons.Select(x => new PersonRelatedPerson
                {
                    RelatedPersonId = x.RelatedPersonId,
                    ReletiveType = x.ReletiveType
                }).ToList();


                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
