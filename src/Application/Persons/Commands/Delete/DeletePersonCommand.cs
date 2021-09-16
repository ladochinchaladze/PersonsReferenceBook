using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons.Commands.Delete
{
    public class DeletePersonCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
            {
                var person = await _context
                    .Persons
                    .FirstAsync(x => x.Id == request.Id);

                person.IsDeleted = true;

                return Unit.Value;
            }
        }
    }
}
