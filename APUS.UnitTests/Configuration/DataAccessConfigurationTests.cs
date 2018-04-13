namespace APUS.UnitTests.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Unity;
    using Xunit;

    public class DataAccessConfigurationTests
    {
        [Fact]
        public void DataAccessConfiguration_ConstructorParameterContainerNullReturnsException()
        {
            var fakeUnityContainer = new UnityContainer();
        }

        [Fact]
        public void DataAccessConfiguration_ConstructorParameterFileNameNullReturnsException()
        {
            var fakeUnityContainer = new UnityContainer();
        }

        [Fact]
        public void DataAccessConfiguration_ConstructorParameterFileNameEmptyReturnsException()
        {
            var fakeUnityContainer = new UnityContainer();
        }

        [Fact]
        public void DataAccessConfiguration_ConstructorParametersNotNullReturnsDataAccessConfiguration()
        {
            var fakeUnityContainer = new UnityContainer();
        }
    }
}