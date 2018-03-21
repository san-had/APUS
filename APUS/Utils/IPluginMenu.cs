namespace APUS.Utils
{
    using System;
    using System.Collections.Generic;

    public interface IPluginMenu
    {
        void DisplayMenu(IList<Type> options);

        Type GetChoise(IList<Type> options);
    }
}