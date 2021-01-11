using FluentAssertions;
using MESH.Api.Client.Authentication;
using MESH.Api.Client.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Net.Http.Headers;

namespace MESH.Api.Client.UnitTests.Client
{
    [TestFixture]
    public class WhenRequestingAnAuthenticationHeader
    {
        [Test, MESHAutoData]
        public void It_Returns_A_Valid_Header(MESHApiClientConfiguration config)
        {
            var expectedBearerToken = $"NHSMESH {config.MailBoxId.ToUpper()}:256e4af8-ca60-4e86-aaa0-8c763658d694:001:202101010000:5171955e6fa23fb7f83ae55756babf2dc4b51a8aee1a54f10f07ea7337198f6c";

            var mockDateTime = new DateTime(2021, 1, 1);
            var mockDateTimeNowProvider = new Mock<IDateTimeNowProvider>();
            mockDateTimeNowProvider.Setup(d => d.UtcNow()).Returns(mockDateTime);

            var mockNonce = Guid.Parse("256e4af8-ca60-4e86-aaa0-8c763658d694");
            var mockGuidProvider = new Mock<IGuidProvider>();
            mockGuidProvider.Setup(g => g.NewGuid()).Returns(mockNonce);

            var authenticationHeaderGenerator = new AuthenticationHeaderGenerator(config, mockDateTimeNowProvider.Object, mockGuidProvider.Object);

            var result = authenticationHeaderGenerator.GetAuthenticationHeader();

            result.Should().BeOfType<AuthenticationHeaderValue>();
            result.ToString().Should().Be(expectedBearerToken);
        }
    }
}
