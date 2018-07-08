using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Fisabrantes.App_Start
{
    public class WebApiConfig
    {
        public static void Register (HttpConfiguration config)
        {
            #region Formatação JSON e XML

            // Desligar o formatador do XML.
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Pretty-print do JSON
            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Newtonsoft.Json.Formatting.Indented;


            #endregion

            #region Routing


            // Attribute routing toma prioriade sobre conventions-based routing (abaixo).
            config.MapHttpAttributeRoutes();

            // O equivalente do "MapRoute" do MVC (ver RouteConfig.cs nesta pasta).

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            #endregion
        }
    }
}