using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFoodCommerce.AccesoDatos
{
    public class UsuarioActual : IUsuarioActual
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsuarioActual(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public string? ObtenerUsuarioActual()
        {
            if (httpContextAccessor.HttpContext == null || httpContextAccessor.HttpContext.User == null || httpContextAccessor.HttpContext.User.Identity == null)
            {
                return null;
            }
            return httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }

}
