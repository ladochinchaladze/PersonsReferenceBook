using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cities.Commands
{
    public class CreateCityCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string NameEng { get; set; }

        public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreateCityCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateCityCommand request, CancellationToken cancellationToken)
            {
                await _context
                    .Cities
                    .AddAsync(new Domain.Entiities.City
                    {
                        Name = request.Name,
                        NameEng = request.NameEng
                    }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
