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
    public class PrecioProductoConfiguracion : IEntityTypeConfiguration<PrecioProducto>
    {
        public void Configure(EntityTypeBuilder<PrecioProducto> builder)
        {

            builder.Property(x => x.Codigo).IsRequired();
            builder.HasIndex(x => x.Codigo).IsUnique();
            builder.Property(x => x.CodigoProducto).IsRequired();
            builder.HasIndex(x => x.CodigoProducto).IsUnique();
            builder.Property(x => x.CodigoTipoPrecio).IsRequired();
            builder.HasIndex(x => x.CodigoTipoPrecio).IsUnique();
            builder.Property(x => x.Precio).IsRequired();

            builder.HasOne(x => x.Producto).WithMany()
                   .HasForeignKey(x => x.CodigoProducto)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.TipoPrecio).WithMany()
                   .HasForeignKey(x => x.CodigoTipoPrecio)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
