using Application.Common.Interfaces;
using Application.Common.Paging;
using Application.Persons.Queries.Filters;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using AutoMapper.QueryableExtensions;

namespace Application.Persons.Queries
{
    public class PersonFullSearchQueryHandler : IRequestHandler<FilterBaseQuery<PersonFullSearchFilter, Common.Paging.PagedResult<PersonsDto>>, Common.Paging.PagedResult<PersonsDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PersonFullSearchQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Common.Paging.PagedResult<PersonsDto>> Handle(FilterBaseQuery<PersonFullSearchFilter, Common.Paging.PagedResult<PersonsDto>> request, CancellationToken cancellationToken)
        {
            var persons = _context
                .Persons
                .Include(x => x.City)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ProjectTo<PersonsDto>(_mapper.ConfigurationProvider);



            persons = persons
                    .Where(x => request.Filter.Id == null ? true : x.Id == request.Filter.Id &&
                     string.IsNullOrEmpty(request.Filter.FirstName) ? true : x.FirstName.Contains(request.Filter.FirstName) &&
                     string.IsNullOrEmpty(request.Filter.LastName) ? true : x.FirstName.Contains(request.Filter.LastName) &&
                     request.Filter.Gender == null ? true : x.Gender == request.Filter.Gender &&
                     string.IsNullOrEmpty(request.Filter.IdentityNumber) ? true : x.FirstName.Contains(request.Filter.IdentityNumber) &&
                     request.Filter.BirthDateFrom == null ? true : x.BirthDate >= request.Filter.BirthDateFrom &&
                     request.Filter.BirthDateTo == null ? true : x.BirthDate <= request.Filter.BirthDateFrom &&
                     string.IsNullOrEmpty(request.Filter.CityName) ? true : x.City.Name.Contains(request.Filter.CityName)
                     );

            if (request.Sorting.SortingBy == SortingDirectionEnum.Ascending)
            {
                persons = persons.OrderBy(request.Sorting.SortingName);
            }
            else
            {
                persons = persons.OrderBy($"{request.Sorting.SortingName} descending");
            }

            return await Common.Paging.PagedResult<PersonsDto>.CreateAsync(persons, request.PageIndex, request.PageSize, cancellationToken);
        }
    }
}
