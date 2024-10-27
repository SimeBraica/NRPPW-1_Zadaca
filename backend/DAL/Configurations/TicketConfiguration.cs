using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations {
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket> {
        public void Configure(EntityTypeBuilder<Ticket> builder) {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("gen_random_uuid()"); 

            builder.Property(c => c.DateCreated)
                    .IsRequired();
        }
    }
}
