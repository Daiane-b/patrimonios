using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using Data;
using Ex2.App_Start;

namespace Ex2
{
    public class auth: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            try
            {
                using(ex2Entities db = new ex2Entities())
                {
                    var resp = db.USUARIO.FirstOrDefault(u => u.email == context.UserName && u.senha == context.Password);

                    if (resp != null)
                    {
                        //identity.AddClaim(new Claim("email".))
                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Usuário incorreto");
                    }
                }
            }
            catch(Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
            }
        }
    }

    public class QueryStringOAuthBearerProvider : OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            string token = context.Request.Query.Get("AcessToken");

            if (!string.IsNullOrEmpty(token))
            {
                var authenticationTicket = Startup.AuthServerOptions.AccessTokenFormat.Unprotect(token);
            }
            return Task.FromResult<object>(null);
        }
    }
}