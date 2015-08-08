using System.Web.Http;

namespace HelloWorldWebApi2
{

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "",
                defaults: new { controller = "HelloWorld" }
            );
        }
    }
}
