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
    public class TarjetaProcesadorPagoConfiguracion : IEntityTypeConfiguration<TarjetaProcesadorPago>
    {
        public void Configure(EntityTypeBuilder<TarjetaProcesadorPago> builder)
        {
            builder.HasKey(x => x.Codigo);

            builder.HasOne(x => x.Tarjeta).WithMany().HasForeignKey(x => x.CodigoTarjeta).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProcesadorPago).WithMany().HasForeignKey(x => x.CodigoProcesadorPago).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}