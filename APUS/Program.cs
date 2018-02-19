namespace APUS
{
    using APUS.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(Constants.GreetingText);

            StartUp();

            Run();
        }

        private static void Run()
        {
            var dataAccess = new DataAccess.CsvDataAccess();

            var dbPresidents = dataAccess.GetDbPresidents();

            var presidents = AutoMapper.Mapper.Map<IEnumerable<DataAccess.DbPresident>, IEnumerable<Models.President>>(dbPresidents);

            var presidentViewList = UpdateViewPresidents(presidents);

            RenderPresidentViewList(presidentViewList);
        }

        private static void StartUp()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperPresidentProfile>());

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }

        private static IEnumerable<ViewModel.PresidentView> UpdateViewPresidents(IEnumerable<Models.President> presidents)
        {
            var presidentViewList = new List<ViewModel.PresidentView>();

            foreach (var president in presidents)
            {
                var viewPresident = new ViewModel.PresidentView();

                viewPresident.FirstName = president.FirstName;
                viewPresident.LastName = president.LastName;
                viewPresident.PresidencyRange = GetPresidencyRange(president.TookOffice, president.LeftOffice);
                viewPresident.NumberOfPresidencyDays = CalculateNumberOfPresidencyDays(president.TookOffice, president.LeftOffice);

                presidentViewList.Add(viewPresident);
            }
            return presidentViewList;
        }

        private static string GetPresidencyRange(DateTime? tookOffice, DateTime? leftOffice)
        {
            var sb = new StringBuilder();

            if (tookOffice.HasValue || leftOffice.HasValue)
            {
                sb.Append("(");
                var tookYear = tookOffice.HasValue ? tookOffice.Value.Year.ToString() : Constants.NAString;
                sb.Append(tookYear);
                sb.Append("-");
                var leftYear = leftOffice.HasValue ? leftOffice.Value.Year.ToString() : Constants.NALeftOfficeString;
                sb.Append(leftYear).Append(")");
            }
            else
            {
                sb.Append(Constants.NAString);
            }

            return sb.ToString();
        }

        private static int CalculateNumberOfPresidencyDays(DateTime? tookOffice, DateTime? leftOffice)
        {
            int presidencyDays = 0;

            if (tookOffice.HasValue && leftOffice.HasValue)
            {
                TimeSpan offset = leftOffice.Value.Subtract(tookOffice.Value);

                presidencyDays = offset.Days;
            }
            if (tookOffice.HasValue && !leftOffice.HasValue)
            {
                DateTime? currentDate = DateTime.Now;

                TimeSpan offset = currentDate.Value.Subtract(tookOffice.Value);

                presidencyDays = offset.Days;
            }

            return presidencyDays;
        }

        private static void RenderPresidentViewList(IEnumerable<PresidentView> presidentViewList)
        {
            Console.WriteLine(Constants.CsvOutputHeader);

            foreach (var presidentView in presidentViewList)
            {
                Console.WriteLine($"{presidentView.LastName.ToUpper()},{presidentView.FirstName},{presidentView.PresidencyRange},{presidentView.NumberOfPresidencyDays} days");
            }
        }
    }
}