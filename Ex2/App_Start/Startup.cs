using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Ex2.App_Start.Startup))]

namespace Ex2.App_Start
{
    public class Startup
    {

        public static OAuthAuthorizationServerOptions AuthServerOptions;


        public Startup()
        {
            AuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new auth()
            };
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);


                //map.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
                //{
                //    Provider = new QueryStringOAuthBearerProvider()
                //});

                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true,
                    EnableDetailedErrors = true,
                    Resolver = GlobalHost.DependencyResolver
                };

                map.RunSignalR(hubConfiguration);
            });


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888


            app.UseOAuthAuthorizationServer(AuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration httpConfig = new HttpConfiguration();
            app.UseWebApi(httpConfig);


        }
    }
}
