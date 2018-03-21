namespace APUS.Utils
{
    public interface IReportConfigurator
    {
        void Setup();

        bool IsSuccesfulConfiguration { get; set; }
    }
}