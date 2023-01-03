namespace Conjoint.Client.Interfaces
{
    public interface IApiService
    {
        Task<SensitivityResponse> GetSensitivityAsync(string country = "100");
    }
}