namespace APUS.Utils
{
    using System.Collections.Generic;

    public interface IMenu
    {
        void DisplayMenu(Dictionary<int, string> options);

        int GetChoise(Dictionary<int, string> options);
    }
}