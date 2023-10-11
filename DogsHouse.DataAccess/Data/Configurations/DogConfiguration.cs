using DogsHouseService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouse.DataAccess.Data.Configurations
{
    public class DogConfiguration : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasIndex(d => d.Id).IsUnique();

            builder.Property(d => d.Name);
            builder.Property(d => d.Color);
            builder.Property(d => d.TailLength);
            builder.Property(d => d.Weight);
        }
    }
}
