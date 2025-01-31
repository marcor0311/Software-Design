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
    public class PrecioProductoPedidoConfiguracion : IEntityTypeConfiguration<PrecioProductoPedido>
    {
        public void Configure(EntityTypeBuilder<PrecioProductoPedido> builder)
        {
            builder.Property(x => x.Codigo).IsRequired();
            builder.HasIndex(x => x.Codigo).IsUnique();
            builder.Property(x => x.CodigoPrecioProducto).IsRequired();
            builder.HasIndex(x => x.CodigoPrecioProducto).IsUnique();
            builder.Property(x => x.CodigoPedido).IsRequired();
            builder.HasIndex(x => x.CodigoPedido).IsUnique();
            builder.Property(x => x.Cantidad).IsRequired();

            builder.HasOne(x => x.PrecioProducto).WithMany()
                   .HasForeignKey(x => x.CodigoPrecioProducto)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Pedido).WithMany()
                   .HasForeignKey(x => x.CodigoPedido)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
