using APUS.Utils;
using APUS.ViewModel;
using System;
using System.Collections.Generic;

namespace APUS
{
    class Program
    {
        static void Main(string[] args)
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
            throw new NotImplementedException();
        }

        private static int CalculateNumberOfPresidencyDays(DateTime? tookOffice, DateTime? leftOffice)
        {
            throw new NotImplementedException();
        }

        private static void RenderPresidentViewList(IEnumerable<PresidentView> presidentViewList)
        {
            throw new NotImplementedException();
        }
    }
}
