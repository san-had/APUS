namespace APUS
{
    using System.Collections.Generic;

    public interface IMenu
    {
        int GetChoise(Dictionary<int, string> options);

        void DisplayMenu(Dictionary<int, string> options);
    }
}