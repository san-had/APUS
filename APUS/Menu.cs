namespace APUS
{
    using System;
    using System.Collections.Generic;

    public class Menu : IMenu
    {
        public void DisplayMenu(Dictionary<int, string> options)
        {
            if (options == null || options.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            foreach (var item in options)
            {
                Console.WriteLine($"{item.Key}. {item.Value}");
            }
            Console.WriteLine();
            Console.WriteLine("Your choice: ");
        }

        public int GetChoise(Dictionary<int, string> options)
        {
            int formatNumber = 0;
            bool isParsed = false;

            if (options == null || options.Count == 0)
            {
                return formatNumber;
            }

            while (!options.ContainsKey(formatNumber) || !isParsed)
            {
                var readString = Console.ReadLine();
                isParsed = int.TryParse(readString.Trim(), out formatNumber);
            }

            return formatNumber;
        }
    }
}