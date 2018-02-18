using APUS.Utils;
using System;
using System.Collections.Generic;

namespace APUS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello APUS!");

            StartUp();

            Run();
        }
        
        private static void Run()
        {
            var dataAccess = new DataAccess.CsvDataAccess();

            var dbPresidents = dataAccess.GetDbPresidents();

            var presidents = AutoMapper.Mapper.Map<IEnumerable<DataAccess.DbPresident>, IEnumerable<Models.President>>(dbPresidents);

        }

        private static void StartUp()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DataAccess.DbPresident, Models.President>()
                    .ForMember(
                                dest => dest.TookOffice,
                                opt => opt.MapFrom(src => src.TookOffice.ParseUsDateFormat()))
                    .ForMember(
                                dest => dest.LeftOffice,
                                opt => opt.MapFrom(src => src.LeftOffice.ParseUsDateFormat()));
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}
