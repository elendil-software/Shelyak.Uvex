using FastEndpoints;
using Microsoft.AspNetCore.Localization;

namespace Shelyak.Uvex.Web.Endpoints.Culture;

public class SwitchCultureEndpoint : EndpointWithoutRequest
{
    internal const string RoutePattern = "/culture/{culture}";
    
    public override void Configure()
    {
        Get(RoutePattern);
        RoutePrefixOverride("");
        Tags("culture");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        string? culture = Route<string>("culture");
        string redirectUri = Query<string>("redirectUri", false) ?? "/";
        
        if (culture != null)
        {
            var requestCulture = new RequestCulture(culture, culture);
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName, 
                CookieRequestCultureProvider.MakeCookieValue(requestCulture),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    SameSite = SameSiteMode.None,
                    Secure = true
                });
        }
        
        await SendRedirectAsync(redirectUri);
    }
}