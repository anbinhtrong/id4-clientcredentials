namespace ClientCredentialExample.Services
{
    public interface IAuthServerService
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}
