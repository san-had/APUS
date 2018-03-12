namespace APUS.OutputFormatters
{
    using APUS.ViewModels;

    public interface IOutputFormatter
    {
        void RenderOutput(OfficerViewModel officerViewModel);
    }
}