using Domain.Entiities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);
            builder.Property(x => x.IdentityNumber).HasMaxLength(11).IsUnicode();

            builder.HasIndex(x => new { x.FirstName, x.LastName, x.IdentityNumber, x.IsDeleted })
                .IsUnique();


            builder.HasIndex(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.IdentityNumber,
                x.Gender,
                x.BirthDate,
                x.IsDeleted
            }).IsUnique();

            builder.HasMany(x => x.Contacts)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);

            builder.HasOne(x => x.City)
                .WithMany(x => x.Persons)
                .HasForeignKey(x => x.CityId);

            builder.HasMany(x => x.PersonRelatedPersons)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
