using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JogosDeGuerraWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuração e serviços de API Web
            // Configure a API Web para usar somente a autenticação de token de portador.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //Possibiltiar a serialização/deserialização de referências circulares.
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //Para possibilitar a deserialização de lista de interfaces e objetos abstratos.
            jsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Objects;

            // Rotas de API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
