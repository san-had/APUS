namespace APUS.UnitTests.Configuration
{
    using APUS.Utils;
    using System.Collections.Generic;

    public class FakeMenu : IMenu
    {
        private int formatNumber = 0;

        public FakeMenu(int formatNumber)
        {
            this.formatNumber = formatNumber;
        }

        public void DisplayMenu(Dictionary<int, string> options)
        {
            return;
        }

        public int GetChoise(Dictionary<int, string> options)
        {
            return this.formatNumber;
        }
    }
}