using Microsoft.AspNetCore.Http;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace EFoodCommerce.Middleware 
{
    public class Excepciones(RequestDelegate requestDelegate)
    {
        private readonly RequestDelegate _requestDelegate = requestDelegate;

        public async Task Invoke(HttpContext httpContext, IUnidadTrabajo unidadTrabajo) {
            try {
                await _requestDelegate(httpContext);
            } catch (Exception ex) {
                try {
                    await unidadTrabajo.Guardar();
                } catch (Exception e) {
                    unidadTrabajo.Deshacer();
                    try {
                        await Handle(httpContext, unidadTrabajo, e);
                    } catch (Exception exx) {
                        throw new Exception("Algo pasó con la base de datos o con la bitácora de errores", exx);
                    }
                }
                await Handle(httpContext, unidadTrabajo, ex);
            }
        }

        private static async Task Handle(HttpContext httpContext, IUnidadTrabajo unidadTrabajo, Exception ex) {
            Error error = new()
            {
                FechaHora = DateTime.Now,
                Mensaje = ex.Message
            };
            if (error.Mensaje.Length > 512) error.Mensaje = error.Mensaje[..512];
            await unidadTrabajo.Error.Agregar(error);
            await unidadTrabajo.Guardar();
            var statusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.StatusCode = statusCode;
        }
    }
}