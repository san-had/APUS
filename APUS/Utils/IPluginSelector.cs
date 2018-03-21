namespace APUS.Utils
{
    using System;
    using System.Collections.Generic;

    public interface IPluginSelector
    {
        Type GetSelected(string fileName, IList<Type> options);
    }
}