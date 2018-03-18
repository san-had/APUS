namespace APUS
{
    using System;
    using System.Collections.Generic;

    public interface IPluginExplorer
    {
        Dictionary<Type, IList<Type>> GetPlugins(string pluginFolder);
    }
}
