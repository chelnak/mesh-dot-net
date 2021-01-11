using System;

namespace MESH.Api.Client.Authentication
{
    public class GuidProvider : IGuidProvider
    {
        public Guid NewGuid() => Guid.NewGuid();
    }
}
