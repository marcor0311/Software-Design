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
    internal class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CodigoLineaComida).IsRequired();
            builder.Property(x => x.Contenido);
            builder.Property(x => x.RutaImagen).IsRequired();

            builder.HasOne(x => x.LineaComida)
                .WithMany()
                .HasForeignKey(x => x.CodigoLineaComida)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
