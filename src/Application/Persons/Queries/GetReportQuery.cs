using Application.Common.Interfaces;
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
    public class GetReportQuery : IRequest<List<ReportVM>>
    {

        public class GetReportQueryHandler : IRequestHandler<GetReportQuery, List<ReportVM>>
        {
            private readonly IApplicationDbContext _context;

            public GetReportQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<ReportVM>> Handle(GetReportQuery request, CancellationToken cancellationToken)
            {
               

                var persons = await _context
                    .Persons
                    .Include(x => x.PersonRelatedPersons)
                    .Where(x => x.IsDeleted == false).ToListAsync(cancellationToken);


                var data = persons
                    .Select(x => new ReportVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Relatives = x.PersonRelatedPersons
                        .GroupBy(g => g.ReletiveType)
                        .Select(r => new RelativeVm
                        {
                            Count = r.Count(),
                            ReletiveType = r.Key
                        }).ToList()
                    });

                return data.ToList();


            }
        }
    }
}
