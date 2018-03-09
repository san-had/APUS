namespace APUS.OutputFormatters
{
    using APUS.ViewModels;
    using System.Collections.Generic;

    public interface IOutputFormatter
    {
        void RenderOutput(IEnumerable<OfficerView> presidentViewList);
    }
}