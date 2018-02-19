namespace APUS.OutputFormatters
{
    using APUS.ViewModel;
    using System.Collections.Generic;

    public interface IOutputFormatter
    {
        void RenderOutput(IEnumerable<PresidentView> presidentViewList);
    }
}
