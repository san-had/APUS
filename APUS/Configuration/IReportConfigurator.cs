namespace APUS.Configuration
{
    public interface IReportConfigurator
    {
        void Setup();

        bool IsSuccesfulConfiguration { get; set; }
    }
}