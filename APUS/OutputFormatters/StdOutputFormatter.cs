namespace APUS.OutputFormatters
{
    using System;
    using System.Collections.Generic;
    using APUS.ViewModels;

    public class StdOutputFormatter : IOutputFormatter
    {
        public void RenderOutput(IEnumerable<PresidentView> presidentViewList)
        {
            Console.WriteLine(Constants.CsvOutputHeader);

            foreach (var presidentView in presidentViewList)
            {
                Console.WriteLine($"{presidentView.LastName.ToUpper()},{presidentView.FirstName},{presidentView.PresidencyRange},{presidentView.NumberOfPresidencyDays} days");
            }
        }
    }
}