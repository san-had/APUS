namespace APUS.Configuration
{
    using APUS.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Unity;
    using Unity.Injection;

    public class DataAccessConfiguration : IConfiguration
    {
        private IUnityContainer container;

        public bool IsSuccesfulDataConfiguration { get; set; } = false;

        private string fileName;

        public DataAccessConfiguration(IUnityContainer container, string fileName)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));

            if (!string.IsNullOrEmpty(fileName))
            {
                this.fileName = fileName;
            }
            else
            {
                throw new ArgumentNullException(nameof(fileName));
            }
        }

        public void Configure()
        {
            ConfigureDataAccess();
        }

        private void ConfigureDataAccess()
        {
            var dictionary = GetDataAccessType(fileName);

            if (dictionary.Count > 0)
            {
                container.RegisterType(dictionary.Keys.First(), dictionary.Values.First(), new InjectionConstructor(fileName));
            }
            else
            {
                IsSuccesfulDataConfiguration = false;
                return;
            }

            var dataAccessPluginName = dictionary.Values.First().Assembly.FullName.Split(',')[0].Trim();

            if (dataAccessPluginName.EndsWith("En"))
            {
                container.RegisterType<DataLoader.IDateParser, DataLoader.EnDateParser>();
            }
            else if (dataAccessPluginName.EndsWith("Us"))
            {
                container.RegisterType<DataLoader.IDateParser, DataLoader.UsDateParser>();
            }
            else if (dataAccessPluginName.EndsWith("Utc"))
            {
                container.RegisterType<DataLoader.IDateParser, DataLoader.UTCDateTimeParser>();
            }
            else
            {
                throw new NotImplementedException($"No available DateTime parser for this dataAccessType: {dataAccessPluginName}");
            }

            IsSuccesfulDataConfiguration = true;
        }

        private Dictionary<Type, Type> GetDataAccessType(string fileName)
        {
            Type typeFrom = null;

            Type typeTo = null;

            var pluginExplorer = new PluginExplorer();

            var typeExploreredCollection = pluginExplorer.GetPlugins(Constants.DataAccessPluginFolder);

            var types = new List<Type>();

            foreach (var items in typeExploreredCollection)
            {
                typeFrom = items.Key;

                foreach (var item in items.Value)
                {
                    types.Add(item);
                }
            }

            var pluginSelector = new PluginSelector();
            typeTo = pluginSelector.GetSelected(fileName, types);

            var mapping = new Dictionary<Type, Type>();

            if (typeFrom != null && typeTo != null)
            {
                mapping.Add(typeFrom, typeTo);
            }

            return mapping;
        }
    }
}