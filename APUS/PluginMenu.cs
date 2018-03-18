namespace APUS
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PluginMenu : IPluginMenu
    {
        public void DisplayMenu(Dictionary<Type, IList<Type>> options)
        {
            foreach (var typeCollection in options)
            {
                for (int i = 0; i < typeCollection.Value.Count; i++)
                {
                    int menuIndex = i + 1;
                    Console.WriteLine($"{menuIndex}. {typeCollection.Value[i].Assembly.FullName}");
                }
            }
        }

        public Type GetChoise(Dictionary<Type, IList<Type>> options)
        {
            int formatNumber = 0;
            bool isParsed = false;

            if (options == null || options.Count == 0)
            {
                return null;
            }

            while (options.Count < formatNumber  || !isParsed)
            {
                var readString = Console.ReadLine();
                isParsed = int.TryParse(readString.Trim(), out formatNumber);
            }

            return options[formatNumber - 1];
        }
    }
}
