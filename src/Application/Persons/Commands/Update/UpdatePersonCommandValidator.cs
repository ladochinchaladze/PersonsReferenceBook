using Application.Common.Interfaces;
using Application.Common.Validators;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persons.Commands.Update
{
    public class UpdatePersonCommandValidator : PersonBaseValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator(IApplicationDbContext context, IStringLocalizerFactory factory) :
            base(context, factory)
        { }
    }
}
