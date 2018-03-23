namespace APUS
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(Constants.GreetingText);

            var filesProcessor = new FileCollectionProcessor();

            filesProcessor.FilesProcessing();
        }
    }
}