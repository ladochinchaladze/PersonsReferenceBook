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

    public class PersonFastSearchQueryHandler : IRequestHandler<FilterBaseQuery<PersonFastSearchFilter, Common.Paging.PagedResult<PersonsDto>>, Common.Paging.PagedResult<PersonsDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PersonFastSearchQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Common.Paging.PagedResult<PersonsDto>> Handle(FilterBaseQuery<PersonFastSearchFilter, Common.Paging.PagedResult<PersonsDto>> request, CancellationToken cancellationToken)
        {
            var persons = _context
                .Persons
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ProjectTo<PersonsDto>(_mapper.ConfigurationProvider);

            var search = request.Filter.Search?.ToLower();

            if (!string.IsNullOrEmpty(search))
            {

                persons = persons
                        .Where(x => x.FirstName.Contains(search) ||
                         x.LastName.Contains(search) ||
                         x.IdentityNumber.Contains(search));
            }

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
