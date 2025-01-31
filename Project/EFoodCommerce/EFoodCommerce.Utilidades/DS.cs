using System.Runtime.InteropServices;

namespace EFoodCommerce.Utilidades
{
    public static class DS
    {
        public const string ROLE_ADMINISTRADOR = "Administrador";
        public const string ROLE_MANTENIMIENTO = "Mantenimiento";
        public const string ROLE_SEGURIDAD = "Seguridad";
        public const string ROLE_CONSULTAS = "Consultas";
        public const string ssCarroCompras = "Sesion carro Compras";
        
        //public static string ImagenRuta {get;} = @"/img/producto/";
        public static readonly string ImagenRuta = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"\img\producto\" : @"/img/producto/";
        public const string ROLE_TESTER = "Tester";
    }
}
