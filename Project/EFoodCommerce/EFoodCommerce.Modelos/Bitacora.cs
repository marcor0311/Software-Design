using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFoodCommerce.Modelos
{
    public class Bitacora : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [StringLength(64)]
        public string NombreUsuario { get; set; } = null!;

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        public int CodigoRegistro { get; set; }

        [Required]
        [MaxLength(256)]
        public string Descripcion { get; set; } = null!;
    }
}
