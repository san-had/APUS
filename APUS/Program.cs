namespace APUS
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Unity;

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static IUnityContainer container;

        private static void Main(string[] args)
        {
            Console.WriteLine(Constants.GreetingText);

            var filesProcessor = new FileCollectionProcessor();

            filesProcessor.FilesProcessing();
        }
    }
}