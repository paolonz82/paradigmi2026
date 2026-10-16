using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;

namespace Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Configurations
{
    public class GenereConfiguration : IEntityTypeConfiguration<Genere>
    {
        public void Configure(EntityTypeBuilder<Genere> builder)
        {
            builder.HasKey(x => x.IdGenere);
            builder.ToTable("Generi");
        }
    }
}
