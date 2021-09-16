using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entiities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons.Commands.Create
{
    public class CreatePersonCommand : IRequest<Unit> , IPersonCommandBase
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public IFormFile Image { get; set; }
        public List<ContactModel> Contacts { get; set; }
        public List<ReletivePersonsModel> ReletivePersons { get; set; }


        public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IImageService _imageService;

            public CreatePersonCommandHandler(IApplicationDbContext context, IMapper mapper, IImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
            }

            public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
            {
                var imageName = _imageService.UploadImage(request.Image);

                var person = new Person
                {
                    BirthDate = request.BirthDate,
                    CityId = request.CityId,

                    Contacts = request.Contacts == null ? null : request.Contacts.Select(x => new Contact
                    {
                        ContactType = x.ContactType,
                        Number = x.Number
                    }).ToList(),

                    FirstName = request.FirstName,
                    Gender = request.Gender,
                    IdentityNumber = request.IdentityNumber,
                    ImageName = imageName,
                    LastName = request.LastName,

                    PersonRelatedPersons = request.ReletivePersons == null ? null : request.ReletivePersons.Select(x => new PersonRelatedPerson
                    {
                        RelatedPersonId = x.RelatedPersonId,
                        ReletiveType = x.ReletiveType
                    }).ToList()
                };

                await _context.Persons.AddAsync(person, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
