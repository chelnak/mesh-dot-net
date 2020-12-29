using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace MESH.Api.Client.Authentication
{
    public interface IAuthenticationHeaderGenerator
    {
       AuthenticationHeaderValue GetAuthenticationHeader();
    }
}
