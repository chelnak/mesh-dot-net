using AutoFixture;
using AutoFixture.NUnit3;
using MESH.Api.Client.Configuration;

namespace MESH.Api.Client.UnitTests
{
    public class MESHAutoDataAttribute : AutoDataAttribute
    {
        public MESHAutoDataAttribute() : base(CreateFixture)
        {
        }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture();
            fixture.Customize<MESHApiClientConfiguration>(c => c
                .With(d => d.BaseUrl, "http://localhost")
                .With(d => d.MailBoxPassword, "P@$$w0rd")
                .With(d => d.MailBoxId, "X26HC005")
                .With(d => d.SharedKey, "shared key")
            );

            return fixture;
        }
    }
}
