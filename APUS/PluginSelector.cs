namespace APUS
{
    using System;
    using System.Collections.Generic;

    public class PluginSelector : IPluginSelector
    {
        public Type GetSelected(string fileName, IList<Type> options)
        {
            Type selectedDataAccessType = null;

            foreach (var dataAccessType in options)
            {
                CommonDataAccess.ICommonDataAccess dataAccess = (CommonDataAccess.ICommonDataAccess)Activator.CreateInstance(dataAccessType, fileName);

                if (selectedDataAccessType == null && dataAccess.CanDo())
                {
                    selectedDataAccessType = dataAccess.GetType();
                }
            }

            return selectedDataAccessType;
        }
    }
}