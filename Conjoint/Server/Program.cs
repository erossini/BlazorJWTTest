using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PSC.Blazor.AuthExtensions.Factories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(181);
});

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("oidc", options.ProviderOptions);
    options.UserOptions.RoleClaim = "role";
})
    .AddAccountClaimsPrincipalFactory<MultipleRoleClaimsPrincipalFactory<RemoteUserAccount>>();

builder.Services.AddAuthorizationCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use((context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            MustRevalidate = true,
            NoCache = true,
            NoStore = true,
        };

    string oidcAuthority = builder.Configuration.GetValue(typeof(string), "oidc:Authority").ToString();
    string mainUrl = "https://*.celloconnect.com/";
#if DEBUG
    mainUrl = "https://localhost:7040";
#endif

    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("Content-Security-Policy",
        $"default-src 'self' {mainUrl} {oidcAuthority} 'ws:' 'wss:' " +
            "https://code.cdn.mozilla.net " +
#if DEBUG
            "https://dc.services.visualstudio.com " +
#endif
            "https://api.celloconnect.com " +
            "'unsafe-inline' 'unsafe-eval'; " +
        $"script-src 'self' {mainUrl} 'unsafe-inline' 'unsafe-eval'; " +
        $"connect-src 'self' {oidcAuthority} https://code.cdn.mozilla.net https://api.celloconnect.com;" +
        $"img-src 'self' {mainUrl} data:; " +
        $"style-src 'unsafe-inline' {mainUrl} " +
            "https://code.cdn.mozilla.net " +
            ";" +
        "base-uri 'self'; " +
        "form-action 'self'; " +
        "frame-ancestors 'self';");
    context.Response.Headers.Add("Referrer-Policy", "same-origin");
    context.Response.Headers.Add("Permissions-Policy", "geolocation=(), microphone=()");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    context.Response.Headers.Add("SameSite", "Strict");

    return next.Invoke();
});

app.UseHttpsRedirection();
app.UseHsts();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();