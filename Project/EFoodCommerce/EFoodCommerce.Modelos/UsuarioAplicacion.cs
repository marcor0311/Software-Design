using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFoodCommerce.Modelos
{
    public class UsuarioAplicacion : IdentityUser, IEntity
    {

        [Required]
        [MaxLength(64)]
        public string Nombre { get; set; } = null!;
        [Required]
        [MaxLength(512)]
        public string Pregunta_Seguridad { get; set; } = null!;
        [Required]
        [MaxLength(512)]
        public string Respuesta_Seguridad { get; set; } = null!;

        [NotMapped]
        public string? Role { get; set; }
        [NotMapped]
        public int Codigo { get; set; }

    }
}
