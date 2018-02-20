namespace APUS.UnitTests.ViewModels
{
    using APUS.ViewModels;
    using Xunit;

    public class PresidentViewLoaderTests
    {
        [Fact]
        public void UpdateViewPresidents_PresidentsCollectionIsNull_ReturnsEmptyPresidentViewList()
        {
            var presidentViewLoader = NSubstitute.Substitute.For<IPresidentViewLoader>();

            var presidentViewList = presidentViewLoader.UpdateViewPresidents(null);

            Assert.Empty(presidentViewList);
        }
    }
}