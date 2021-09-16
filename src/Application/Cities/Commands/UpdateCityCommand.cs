using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cities.Commands
{
    public class UpdateCityCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }

        public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public UpdateCityCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
            {
                var city = await _context
                    .Cities
                    .FirstAsync(x => x.Id == request.Id, cancellationToken);

                city.Name = request.Name;
                city.NameEng = request.NameEng;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
