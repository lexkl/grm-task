using Xunit;
using Moq;
using GrmTask.Application.Services;
using GrmTask.Domain;
using GrmTask.Application.Interfaces;

namespace GrmTask.Tests.Services
{
    public class GrmRunnerTests
    {
        [Fact]
        public void Run_ExistingPartner_ReturnsFilteredContracts()
        {
            var musicContracts = new List<MusicContract>
            {
                new MusicContract("Artist1", "Song1", new List<UsageType>{ UsageType.DigitalDownload }, new DateTime(2023,1,1), null),
                new MusicContract("Artist2", "Song2", new List<UsageType>{ UsageType.Streaming }, new DateTime(2023,2,1), null)
            };

            var partnerContracts = new List<PartnerContract>
            {
                new PartnerContract("ITunes", UsageType.DigitalDownload)
            };

            var musicLoaderMock = new Mock<IMusicContractLoader>();
            musicLoaderMock.Setup(x => x.Load(It.IsAny<string>())).Returns(musicContracts);

            var partnerLoaderMock = new Mock<IPartnerContractLoader>();
            partnerLoaderMock.Setup(x => x.Load(It.IsAny<string>())).Returns(partnerContracts);

            var filterService = new ContractFilterService();
            var runner = new GrmRunner(musicLoaderMock.Object, partnerLoaderMock.Object, filterService);

            var results = runner.Run("ITunes", new DateTime(2023, 3, 1)).ToList();

            Assert.Single(results);
            Assert.Equal("Artist1", results[0].Contract.Artist);
            Assert.Equal(UsageType.DigitalDownload, results[0].Usage);
        }

        [Fact]
        public void Run_NonExistingPartner_ThrowsException()
        {
            var musicLoaderMock = new Mock<IMusicContractLoader>();
            musicLoaderMock.Setup(x => x.Load(It.IsAny<string>())).Returns(new List<MusicContract>());

            var partnerLoaderMock = new Mock<IPartnerContractLoader>();
            partnerLoaderMock.Setup(x => x.Load(It.IsAny<string>())).Returns(new List<PartnerContract>());

            var filterService = new ContractFilterService();
            var runner = new GrmRunner(musicLoaderMock.Object, partnerLoaderMock.Object, filterService);

            Assert.Throws<InvalidOperationException>(() =>
                runner.Run("NonExistentPartner", DateTime.Now));
        }
    }
}