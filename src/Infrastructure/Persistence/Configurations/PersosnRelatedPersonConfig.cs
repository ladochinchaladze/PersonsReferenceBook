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
    public class PersosnRelatedPersonConfig : IEntityTypeConfiguration<PersonRelatedPerson>
    {
        public void Configure(EntityTypeBuilder<PersonRelatedPerson> builder)
        {
            builder.ToTable("PersosnRelatedPersons");

            builder.HasKey(x => x.Id);

        }
    }
}
