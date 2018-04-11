namespace APUS.Logging
{
    using System.Runtime.CompilerServices;

    public interface ILogging
    {
        void WriteLog([CallerMemberName] string callerName = null);
    }
}