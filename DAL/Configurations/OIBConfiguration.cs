using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Configurations {
    public class OIBConfiguration : IEntityTypeConfiguration<OIB> {
        public void Configure(EntityTypeBuilder<OIB> builder) {

            builder.HasKey(c => c.Vatin);

            builder.Property(c => c.Firstname)
                    .IsRequired();

            builder.Property(c => c.Lastname)
                   .IsRequired();

            builder.HasMany(c => c.Tickets)
                   .WithOne(c => c.Oib)
                   .HasForeignKey(c => c.Vatin)
                   .IsRequired();
        }
    }
}
