
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons.Queries
{
    public class GetPersonQuery : IRequest<PersonsDto>
    {
        public int Id { get; set; }


        public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonsDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPersonQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PersonsDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
            {
                var person = await _context
                    .Persons
                    .Include(x => x.Contacts)
                    .Where(x => x.IsDeleted == false)
                    .ProjectTo<PersonsDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


                return person;
            }
        }
    }
}
