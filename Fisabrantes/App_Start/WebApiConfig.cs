using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Fisabrantes
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            #region Formatação JSON e XML

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            // Opcional: Converter os nomes de propriedades PascalCase (a la .net)
            // para camelCase (a la Java/JavaScript).
            // Descomentar as duas seguintes linhas mudará o JSON devolvido.

            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            //    new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            #endregion

            #region Routing

            // Configuração do Attribute Routing para Web API.
            config.MapHttpAttributeRoutes();


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            #endregion
        }
    }
}
