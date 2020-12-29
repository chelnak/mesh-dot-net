using System.Net.Http.Headers;

namespace MESH.Api.Client.Authentication
{
    public interface IAuthenticationHeaderGenerator
    {
        AuthenticationHeaderValue GetAuthenticationHeader();
    }
}
