namespace APUS.DataAccess
{
    using Newtonsoft.Json;

    [JsonObject]
    public class Mayor
    {
        [JsonProperty("first")]
        public string FirstName { get; set; }

        [JsonProperty("last:")]
        public string LastName { get; set; }

        [JsonProperty("from")]
        public string TakeOffice { get; set; }

        [JsonProperty("to")]
        public string LeftOffice { get; set; }
    }
}