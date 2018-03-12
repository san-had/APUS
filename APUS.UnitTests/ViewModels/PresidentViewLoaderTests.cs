namespace APUS.UnitTests.ViewModels
{
    using APUS.Models;
    using APUS.ViewModels;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class OfficerViewLoaderTests
    {
        [Fact]
        public void UpdateViewOfficers_OfficersCollectionIsNull_ReturnsEmptyOfficerViewList()
        {
            var fakeOfficerViewCalculator = Substitute.For<IOfficerViewCalculator>();

            var officerViewLoader = new OfficerViewLoaderFirstFormat(fakeOfficerViewCalculator);

            var officerViewList = officerViewLoader.UpdateViewOfficers(null);

            Assert.Empty(officerViewList);
        }

        [Fact]
        public void UpdateViewOfficers_OfficersCollectionIsEmpty_ReturnsEmptyOfficerViewList()
        {
            var fakeOfficerViewCalculator = Substitute.For<IOfficerViewCalculator>();

            var officerViewLoader = new OfficerViewLoaderFirstFormat(fakeOfficerViewCalculator);

            var officerViewList = officerViewLoader.UpdateViewOfficers(Enumerable.Empty<Officer>());

            Assert.Empty(officerViewList);
        }

        [Fact]
        public void UpdateViewOfficers_OfficersCollectionIsNotNull_ReturnsNotEmptyOfficerViewList()
        {
            var fakeOfficerViewCalculator = Substitute.For<IOfficerViewCalculator>();

            var officerViewLoader = new OfficerViewLoaderFirstFormat(fakeOfficerViewCalculator);

            var officerList = new List<Officer>
            {
                new Officer
                {
                    FirstName = "Firstname",
                    LastName = "LastName",
                    TookOffice = new System.DateTime(1789,4,3),
                    LeftOffice = new System.DateTime(1793,4,5),
                    Party = "Independent"
                }
            };

            var officerViewList = officerViewLoader.UpdateViewOfficers(officerList);

            Assert.NotEmpty(officerViewList);
        }
    }
}