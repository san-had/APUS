namespace APUS.Utils
{
    using System;
    using System.Collections.Generic;

    public class PluginMenu : IPluginMenu
    {
        public void DisplayMenu(IList<Type> options)
        {
            if (options == null || options.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            int menuIndex = 1;
            foreach (var type in options)
            {
                Console.WriteLine($"{menuIndex}. {type.Assembly.FullName.Split(',')[0].Trim()}");
                menuIndex++;
            }
            Console.WriteLine();
            Console.Write("Your choice: ");
        }

        public Type GetChoise(IList<Type> options)
        {
            int formatNumber = 0;
            bool isParsed = false;

            if (options == null || options.Count == 0)
            {
                return null;
            }

            while (options.Count < formatNumber || !isParsed)
            {
                var readString = Console.ReadLine();
                isParsed = int.TryParse(readString.Trim(), out formatNumber);
            }

            return options[--formatNumber];
        }
    }
}