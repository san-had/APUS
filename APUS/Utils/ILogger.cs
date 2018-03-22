﻿namespace APUS.Utils
{
    public interface ILogger
    {
        void Log(string message);

        int LogCounter { get; set; }

        void IncrementCounter();
    }
}