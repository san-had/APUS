namespace APUS
{
    using System;
    using System.Collections.Generic;    

    public interface IPluginMenu
    {
        void DisplayMenu(Dictionary<Type,IList<Type>> options);

        Type GetChoise(Dictionary<Type, IList<Type>> options);
    }
}
