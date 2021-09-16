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
    public class DeleteCityCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeleteCityCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
            {
                var city = await _context
                    .Cities
                    .FirstAsync(x => x.Id == request.Id, cancellationToken);

                city.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
