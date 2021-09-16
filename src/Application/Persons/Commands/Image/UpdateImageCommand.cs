using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons.Commands.Image
{
    public class UpdateImageCommand : IRequest<string>
    {
        public int PersonId { get; set; }
        public IFormFile Image { get; set; }


        public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IImageService _imageService;

            public UpdateImageCommandHandler(IApplicationDbContext context, IImageService imageService)
            {
                _context = context;
                _imageService = imageService;
            }

            public async Task<string> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
            {
                var person = await _context
                    .Persons
                    .FirstAsync(x => x.Id == request.PersonId, cancellationToken);

                var newImageName = _imageService.UploadImage(request.Image);
                _imageService.DeleteImage(person.ImageName);

                person.ImageName = newImageName;

                await _context.SaveChangesAsync(cancellationToken);

                return newImageName;


            }
        }
    }
}
