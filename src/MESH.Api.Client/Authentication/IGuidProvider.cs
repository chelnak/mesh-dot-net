using System;

namespace MESH.Api.Client.Authentication
{
    public interface IGuidProvider
    {
        Guid NewGuid();
    }
}
