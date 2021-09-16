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
    public class GetPersonsQuery : IRequest<List<PersonsDto>>
    {

        public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, List<PersonsDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPersonsQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<PersonsDto>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
            {
                var persons = await _context
                    .Persons
                    .Include(x => x.Contacts)
                    .Include(x => x.PersonRelatedPersons)
                    .Where(x => x.IsDeleted == false)
                    .AsNoTracking()
                    .ProjectTo<PersonsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return persons;
            }
        }
    }
}
