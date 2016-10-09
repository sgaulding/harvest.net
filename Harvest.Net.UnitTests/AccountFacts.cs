namespace Harvest.Net.UnitTests
{
    using Harvest.Net.Models;
    using Harvest.Net.Network;
    using Harvest.Net.Utilities;

    using Moq;

    using RestSharp;

    using Xunit;

    public class AccountFacts
    {
        [Fact]
        public void AccountFacts_WhoAmI_ReturnsAccountDetails_Test()
        {
            // Arrange
            const string Subdomain = "Test";
            const string Username = "Test";
            const string Password = "Test";

            var mockAssemblyInfo = new Mock<IAssemblyInformation>();
            var mockEnvironmentInfo = new Mock<IEnvironmentInformation>();

            var mockRestResponse = new Mock<IRestResponse<Account>>();
            mockRestResponse.Setup(_ => _.Data).Returns(new Account());

            var mockRestClient = new Mock<IRestClient>();
            mockRestClient.Setup(_ => _.Execute<Account>(It.IsAny<IRestRequest>()))
                .Returns(() => mockRestResponse.Object);

            var mockRestSharpFactory = new Mock<IRestSharpFactory>();
            mockRestSharpFactory.Setup(
                    _ => _.GetWebClient(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(mockRestClient.Object);

            var api = new HarvestRestClient(
                          Subdomain,
                          Username,
                          Password,
                          mockAssemblyInfo.Object,
                          mockEnvironmentInfo.Object,
                          mockRestSharpFactory.Object);

            // Act
            var account = api.WhoAmI();

            // Assert
            Assert.NotNull(account);
        }
    }
}