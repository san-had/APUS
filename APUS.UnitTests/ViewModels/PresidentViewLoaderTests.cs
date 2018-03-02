namespace APUS.UnitTests.ViewModels
{
    using APUS.Models;
    using APUS.ViewModels;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class PresidentViewLoaderTests
    {
        [Fact]
        public void UpdateViewPresidents_PresidentsCollectionIsNull_ReturnsEmptyPresidentViewList()
        {
            var fakePresidentViewCalculator = Substitute.For<IPresidentViewCalculator>();

            var presidentViewLoader = new PresidentViewLoader(fakePresidentViewCalculator);

            var presidentViewList = presidentViewLoader.UpdateViewPresidents(null);

            Assert.Empty(presidentViewList);
        }

        [Fact]
        public void UpdateViewPresidents_PresidentsCollectionIsEmpty_ReturnsEmptyPresidentViewList()
        {
            var fakePresidentViewCalculator = Substitute.For<IPresidentViewCalculator>();

            var presidentViewLoader = new PresidentViewLoader(fakePresidentViewCalculator);

            var presidentViewList = presidentViewLoader.UpdateViewPresidents(Enumerable.Empty<President>());

            Assert.Empty(presidentViewList);
        }

        [Fact]
        public void UpdateViewPresidents_PresidentsCollectionIsNotNull_ReturnsNotEmptyPresidentViewList()
        {
            var fakePresidentViewCalculator = Substitute.For<IPresidentViewCalculator>();

            var presidentViewLoader = new PresidentViewLoader(fakePresidentViewCalculator);

            var presidentList = new List<President>
            {
                new President
                {
                    FirstName = "Firstname",
                    LastName = "LastName",
                    TookOffice = new System.DateTime(1789,4,3),
                    LeftOffice = new System.DateTime(1793,4,5),
                    Party = "Independent"
                }
            };

            var presidentViewList = presidentViewLoader.UpdateViewPresidents(presidentList);

            Assert.NotEmpty(presidentViewList);
        }
    }
}