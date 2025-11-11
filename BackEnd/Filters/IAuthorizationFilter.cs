using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace TaskManagement.Filters
{
    // Custom attribute to mark actions that require API Key authentication
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthFilter : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeaderName = "X-API-KEY";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var requiredApiKey = configuration.GetValue<string>("Authentication:ApiKey");

            if (string.IsNullOrEmpty(requiredApiKey))
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiKeyAuthFilter>>();
                logger.LogError("API Key configuration is missing or empty. Check 'Authentication:ApiKey' in appsettings.json.");

                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!requiredApiKey.Equals(extractedApiKey, StringComparison.Ordinal))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}