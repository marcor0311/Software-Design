using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFoodCommerce.Modelos
{
    public class TarjetaProcesadorPago : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public int CodigoTarjeta { get; set; }

        [ForeignKey("CodigoTarjeta")]
        public Tarjeta? Tarjeta { get; set; } // Navigation property

        [Required]
        public int CodigoProcesadorPago { get; set; }

        [ForeignKey("CodigoProcesadorPago")]
        public ProcesadorPago? ProcesadorPago { get; set; } // Navigation property
    }
}
