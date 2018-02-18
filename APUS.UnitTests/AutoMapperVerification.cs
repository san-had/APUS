namespace APUS.UnitTests
{
    using Xunit;

    public class AutoMapperVerification
    {
        [Fact]
        public void VerifyMapperConfiguration()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperPresidentProfile>();
            });

            AutoMapper.Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
