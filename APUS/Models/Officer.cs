namespace APUS.Models
{
    using System;

    public class Officer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? TookOffice { get; set; }

        public DateTime? LeftOffice { get; set; }

        public string Party { get; set; }
    }
}