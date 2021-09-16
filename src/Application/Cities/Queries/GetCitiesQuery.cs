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

namespace Application.Cities.Queries
{
    public class GetCitiesQuery : IRequest<List<CityDto>>
    {

        public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, List<CityDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
            {
                var cities = await _context
                    .Cities
                    .Where(x => x.IsDeleted == false)
                    .AsNoTracking()
                    .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return cities;
            }
        }
    }
}
