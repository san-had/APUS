namespace APUS.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Officer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? TookOffice { get; set; }

        public DateTime? LeftOffice { get; set; }

        public string Party { get; set; }

        public string OfficerType { get; set; }
    }
}