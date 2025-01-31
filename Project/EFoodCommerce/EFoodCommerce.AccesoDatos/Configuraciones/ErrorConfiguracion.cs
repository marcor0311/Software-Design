using EFoodCommerce.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFoodCommerce.AccesoDatos.Configuraciones
{
    public class ErrorConfiguracion : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.FechaHora).IsRequired();
            builder.Property(x => x.Mensaje).IsRequired().HasMaxLength(512);
        }
    }
}
