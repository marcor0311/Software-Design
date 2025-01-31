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
    public class TipoPrecioConfiguracion : IEntityTypeConfiguration<TipoPrecio>
    {
        public void Configure(EntityTypeBuilder<TipoPrecio> builder)
        {
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(70);
        }
    }
}