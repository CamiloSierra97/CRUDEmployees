using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;



namespace CRUDEmployees
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // API web services and configuration

            // API web Routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Enable CORS
            EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:5173", "*", "*");
            config.EnableCors(cors);

        }
        }
    
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(config =>
            {
                // Send in serialized .json
                config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }
        public string error = "";
    }
}



