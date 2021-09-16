using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entiities
{
    public class City : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public bool IsDeleted { get; set; }

        public List<Person> Persons { get; set; }
    }
}
