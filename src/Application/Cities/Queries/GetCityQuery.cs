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
    public class GetCityQuery : IRequest<CityDto>
    {
        public int Id { get; set; }
        public class GetCityQueryHandler : IRequestHandler<GetCityQuery, CityDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCityQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CityDto> Handle(GetCityQuery request, CancellationToken cancellationToken)
            {
                var city = await _context
                    .Cities
                    .Where(x=>x.IsDeleted == false)
                    .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                    .FirstAsync(x => x.Id == request.Id, cancellationToken);

                return city;
            }
        }
    }
}
