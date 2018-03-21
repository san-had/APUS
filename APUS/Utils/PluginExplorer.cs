namespace APUS.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class PluginExplorer : IPluginExplorer
    {
        public Dictionary<Type, IList<Type>> GetPlugins(string pluginFolder)
        {
            string DependenciesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.DataAccessPluginFolder);
            string[] Dependencies = Directory.GetFiles(DependenciesPath, Constants.DllPattern);

            Dictionary<Type, IList<Type>> pluginMappings = new Dictionary<Type, IList<Type>>();

            foreach (string fileName in Dependencies)
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(fileName);
                if (assemblyName != null)
                {
                    Assembly pluginAssembly = Assembly.LoadFrom(fileName);

                    foreach (Type pluginType in pluginAssembly.GetTypes())
                    {
                        if (pluginType.IsPublic)
                        {
                            if (!pluginType.IsAbstract)
                            {
                                Type[] typeInterfaces = pluginType.GetInterfaces();
                                foreach (Type typeInterface in typeInterfaces)
                                {
                                    if (pluginMappings.ContainsKey(typeInterface))
                                    {
                                        pluginMappings[typeInterface].Add(pluginType);
                                    }
                                    else
                                    {
                                        var pluginTypeList = new List<Type>();
                                        pluginTypeList.Add(pluginType);
                                        pluginMappings.Add(typeInterface, pluginTypeList);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return pluginMappings;
        }
    }
}