namespace APUS.DataAccess.DbModels
{
    using Newtonsoft.Json;

    [JsonObject]
    public class DbMayor
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