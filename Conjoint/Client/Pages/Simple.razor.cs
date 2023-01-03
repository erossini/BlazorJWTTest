using Microsoft.AspNetCore.Components.Authorization;
using PSC.Blazor.AuthExtensions.Extensions;

namespace Conjoint.Client.Pages
{
    public partial class Simple
    {
        #region Variables

        SensitivityResponse? sentivityResponse;
        string ReadResponse;

        #endregion

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var hasRole = authState.User.Claims.HasRole($"{settings.ProjectNumber}-user");
            if (!hasRole)
                NavManager.NavigateTo("Unauthorized");

            await DrawGraph();
        }

        private async Task DrawGraph()
        {
            await ReadSensitivity();
        }

        private async Task ReadSensitivity()
        {
            sentivityResponse = await api.GetSensitivityAsync();

            ReadResponse = sentivityResponse.Success ? "Working!" : "Error";
        }
    }
}