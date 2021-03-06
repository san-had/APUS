﻿namespace APUS.Configuration
{
    using APUS.Utils;
    using APUS.ViewModels.Calculation;
    using System;
    using System.Collections.Generic;
    using Unity;
    using Unity.Injection;

    public class ViewModelLoaderConfiguration : IConfiguration
    {
        private IUnityContainer container;

        private IMenu menu;

        public ViewModelLoaderConfiguration(IUnityContainer container, IMenu menu)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));

            this.menu = menu ?? throw new ArgumentNullException(nameof(menu));
        }

        public void Configure()
        {
            container.RegisterType<IInOfficeDaysCalculator, InOfficeDaysCalculator>();
            container.RegisterType<IInOfficeRangeComposer, InOfficeRangeComposer>();
            container.RegisterType<ILeftOfficeParser, LeftOfficeParser>();

            int viewFormatNumber = GetViewFormat();

            switch (viewFormatNumber)
            {
                case 1:
                    container.RegisterType<ViewModels.IOfficerViewModelDataMapper, ViewModels.OfficerViewModelLoaderFirstFormat>
                        (
                            new InjectionProperty("InOfficeDaysCalculator"),
                            new InjectionProperty("InOfficeRangeComposer"),
                            new InjectionProperty("LeftOfficeParser"));
                    break;

                case 2:
                    container.RegisterType<ViewModels.IOfficerViewModelDataMapper, ViewModels.OfficerViewModelLoaderSecondFormat>();
                    break;

                default:
                    throw new NotImplementedException($"Invalid viewFormatNumber: {viewFormatNumber.ToString()}");
            }
        }

        private int GetViewFormat()
        {
            var menuDictionary = new Dictionary<int, string>()
            {
                {1, "First Format" },
                {2, "Second Format" }
            };

            menu.DisplayMenu(menuDictionary);
            return menu.GetChoise(menuDictionary);
        }
    }
}