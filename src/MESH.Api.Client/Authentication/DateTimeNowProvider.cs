using System;

namespace MESH.Api.Client.Authentication
{
    public class DateTimeNowProvider : IDateTimeNowProvider
    {
        public DateTime UtcNow() => DateTime.UtcNow;
    }
}
