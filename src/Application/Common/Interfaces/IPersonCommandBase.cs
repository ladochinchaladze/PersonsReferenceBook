

using Application.Common.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IPersonCommandBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public List<ContactModel> Contacts { get; set; }
        public List<ReletivePersonsModel> ReletivePersons { get; set; }
    }
}
