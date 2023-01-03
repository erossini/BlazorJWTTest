using Blazored.LocalStorage;
using Conjoint.Client;
using Conjoint.Client.Handlers;
using Conjoint.Client.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PSC.Blazor.AuthExtensions.Factories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region Read configuration

var configuration = builder.Configuration;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", true, true);

string? apiEndpoint = builder.Configuration["Api:EndpointsUrl"];
string? apiScope = builder.Configuration["Api:Scope"];

ApplicationSettingsModel settings = new ApplicationSettingsModel();
builder.Configuration.Bind(settings);

#endregion Read configuration

#region Dependecy injection

builder.Services.AddTransient(_ =>
{
    return builder.Configuration.GetSection("ApplicationSettings").Get<ApplicationSettingsModel>();
});

builder.Services.AddSingleton<LocalStorageService>();

builder.Services.AddScoped<ApiTokenCacheService>();

builder.Services.AddTransient<UIAuthorizationMessageHandler>();

builder.Services.AddBlazoredLocalStorage();

#endregion Dependecy injection

#region Configure HTTP Client

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IApiService, ApiService>("projectAPI", cl =>
{
    cl.BaseAddress = new Uri(apiEndpoint);
}).AddHttpMessageHandler<UIAuthorizationMessageHandler>();
builder.Services.AddTransient(sp => sp.GetService<IHttpClientFactory>()?.CreateClient("projectAPI"));

#endregion Configure HTTP Client

#region Configure Authentication and Authorization

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("oidc", options.ProviderOptions);
    options.UserOptions.RoleClaim = "role";
})
.AddAccountClaimsPrincipalFactory<MultipleRoleClaimsPrincipalFactory<RemoteUserAccount>>();

builder.Services.AddAuthorizationCore();

#endregion Configure Authentication and Authorization

#if !DEBUG
builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endif

await builder.Build().RunAsync();
