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
    internal class ProcesadorPagoConfiguracion : IEntityTypeConfiguration<ProcesadorPago>
    {
        public void Configure(EntityTypeBuilder<ProcesadorPago> builder)
        {
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.NombreProcesador).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Tipo).IsRequired().HasMaxLength(50);
            builder.Property(x => x.NombreOpcionPago).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.Verificacion).IsRequired();
            builder.Property(x => x.Metodo).IsRequired();

            // Agregar validación de tipo de pago personalizada
            builder.HasIndex(x => x.NombreProcesador).IsUnique();
        }
    }
}
