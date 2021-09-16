

using Domain.Entiities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonRelatedPerson> PersonRelatedPersons { get; set; }
        public DbSet<Contact> Phones { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
