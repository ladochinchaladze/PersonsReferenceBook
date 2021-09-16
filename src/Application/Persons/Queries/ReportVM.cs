using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persons.Queries
{
    public class ReportVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RelativeVm> Relatives { get; set; }
    }

    public class RelativeVm
    {
        public ReletiveTypeEnum ReletiveType { get; set; }
        public int Count { get; set; }
    }
}
