using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Conjoint.Client.Handlers
{
    public class UIAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public UIAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigation, IConfiguration configuration) :
            base(provider, navigation)
        {
            string apiEndpoint = configuration["Api:EndpointsUrl"];
            string apiScope = configuration["Api:Scope"];

            ConfigureHandler(authorizedUrls: new[] { apiEndpoint },
                scopes: new[] { apiScope });
        }
    }
}