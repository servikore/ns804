using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Models;

namespace WebAPI.App_Start
{
    
}

public class JwtValidationHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,     CancellationToken cancellationToken)
    {
        HttpStatusCode statusCode;        

        if (!TryRetrieveToken(request, out string token))
        {            
            return base.SendAsync(request, cancellationToken);
        }

        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(JWTConstants.Secret));
            
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidAudience = JWTConstants.Audience,
                ValidIssuer = JWTConstants.Issuer,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,                
                LifetimeValidator = this.LifetimeValidator,
                IssuerSigningKey = securityKey
            };
            
            Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityTokenPrincipal);
            HttpContext.Current.User = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityTokenUser);

            return base.SendAsync(request, cancellationToken);
        }
        catch (SecurityTokenValidationException)
        {
            statusCode = HttpStatusCode.Unauthorized;
        }
        catch (Exception)
        {
            statusCode = HttpStatusCode.InternalServerError;
        }

        return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
    }
    
    private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
    {
        token = null;
        IEnumerable<string> authzHeaders;
        if (!request.Headers.TryGetValues("Authorization", out authzHeaders) ||
                                          authzHeaders.Count() > 1)
        {
            return false;
        }
        var bearerToken = authzHeaders.ElementAt(0);
        token = bearerToken.StartsWith("Bearer ") ?
                bearerToken.Substring(7) : bearerToken;

        return true;
    }
    public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
    {
        return ((expires.HasValue && DateTime.UtcNow < expires) && (notBefore.HasValue && DateTime.UtcNow > notBefore));
    }
}