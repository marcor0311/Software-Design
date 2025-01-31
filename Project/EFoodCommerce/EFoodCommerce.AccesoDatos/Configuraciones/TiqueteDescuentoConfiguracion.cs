using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFoodCommerce.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFoodCommerce.AccesoDatos.Configuracion
{
    public class TiqueteDescuentoConfiguracion : IEntityTypeConfiguration<TiqueteDescuento>
    {
        public void Configure(EntityTypeBuilder<TiqueteDescuento> builder)
        {
            builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Nombre).IsRequired();
            builder.Property(x => x.Descuento).IsRequired();
            builder.Property(x => x.Disponibles).IsRequired();
        }
    }
}
