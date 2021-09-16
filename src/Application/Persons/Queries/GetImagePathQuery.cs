using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons.Queries
{
    public class GetImagePathQuery : IRequest<string>
    {
        public int PersonId { get; set; }

        public class GetImageQueryHandler : IRequestHandler<GetImagePathQuery, string>
        {
            private readonly IImageService _imageService;
            private readonly IApplicationDbContext _context;

            public GetImageQueryHandler(IImageService imageService, IApplicationDbContext context)
            {
                _imageService = imageService;
                _context = context;
            }

            public async Task<string> Handle(GetImagePathQuery request, CancellationToken cancellationToken)
            {
                var person = await _context
                    .Persons
                    .FirstAsync(x => x.Id == request.PersonId);

                var url = _imageService.GetImagePath(person.ImageName);

                return url;
            }
        }
    }
}
