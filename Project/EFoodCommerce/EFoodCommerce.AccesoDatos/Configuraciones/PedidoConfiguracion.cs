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
    public class PedidoConfiguracion : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.Property(x => x.Codigo).IsRequired();
            builder.HasIndex(x => x.Codigo).IsUnique();
            builder.Property(x => x.Monto).IsRequired();
            builder.Property(x => x.FechaHora).IsRequired();
            builder.Property(x => x.Estado).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CodigoDescuento).IsRequired();

            builder.HasOne(x => x.TiqueteDescuento).WithMany()
                   .HasForeignKey(x => x.CodigoDescuento)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
