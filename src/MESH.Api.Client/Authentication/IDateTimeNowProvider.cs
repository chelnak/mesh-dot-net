using System;

namespace MESH.Api.Client.Authentication
{
    public interface IDateTimeNowProvider
    {
        DateTime UtcNow();
    }
}
