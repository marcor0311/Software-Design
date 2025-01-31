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
    public class BitacoraConfiguracion : IEntityTypeConfiguration<Bitacora>
    {
        public void Configure(EntityTypeBuilder<Bitacora> builder)
        {
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.NombreUsuario).IsRequired().HasMaxLength(60);
            builder.Property(x => x.FechaHora).IsRequired();
            builder.Property(x => x.CodigoRegistro).IsRequired();
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(200);
        }
    }
}
