using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Validators
{
    public class PersonBaseValidator<T> : AbstractValidator<T> where T : IPersonCommandBase
    {
        private readonly IApplicationDbContext _context;

        public PersonBaseValidator(IApplicationDbContext context, IStringLocalizerFactory factory)
        {
            _context = context;

            var type = typeof(Application.Res.Resources.ErrorResources);
            IStringLocalizer localizer = factory.Create(type);

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Matches("^[a-zA-Z]*$|^[ა-ჰ]*$")
                .WithMessage(localizer["FirstNameIsNotValid"]);

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Matches("^[a-zA-Z]*$|^[ა-ჰ]*$")
                .WithMessage(localizer["LastNameIsNotValid"]);

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage(localizer["GenderIsNotValid"]);

            RuleFor(x => x.IdentityNumber)
               .NotNull()
               .NotEmpty()
               .Length(11)
               .WithMessage(localizer["IdentityNumberIsNotValid"]);

            RuleFor(x => x.BirthDate)
                .Must((o, birthDate) => { return IsAdult(birthDate); })
                .WithMessage(localizer["PersonIsNotAdult"]);

            RuleFor(x => x.CityId)
                .Must((o, CityId) => { return IsCityInDb(CityId); })
                .WithMessage(localizer["CityDoesn'tExist"]);


            RuleForEach(c => c.Contacts)
                .ChildRules(x => x.RuleFor(ct => ct.ContactType).IsInEnum())
                .WithMessage(localizer["ContactTypeIsNotValid"]);

            RuleForEach(c => c.Contacts)
                .ChildRules(x => x.RuleFor(ct => ct.Number).MinimumLength(4).MaximumLength(50))
                .WithMessage(localizer["NumberIsNotValid"]);

            RuleForEach(c => c.ReletivePersons)
                .ChildRules(x => x.RuleFor(ct => ct.ReletiveType).IsInEnum())
                .WithMessage(localizer["ReletivePersonTypeIsNotValid"]);

            RuleForEach(c => c.ReletivePersons)
               .ChildRules(x => x.RuleFor(ct => ct.RelatedPersonId)
               .Must((o, RelatedPersonId) => { return PersonExists(RelatedPersonId); }))
               .WithMessage(localizer["RelatedPersonDoesn'tExist"]);

        }

        private bool IsAdult(DateTime birthDate)
        {
            var today = DateTime.Today;

            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age)) age--;

            return age >= 18;
        }

        private bool IsCityInDb(int id)
        {
            return _context.Cities.Any(x => x.Id == id);
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(x => x.Id == id && x.IsDeleted == false);
        }
    }
}
