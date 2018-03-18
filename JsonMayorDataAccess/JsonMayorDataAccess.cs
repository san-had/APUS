namespace JsonMayorDataAccess
{
    using CommonDataAccess;
    using APUS.DataAccess.DbModels;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public class JsonMayorDataAccess : ICommonDataAccess
    {
        public IEnumerable<CommonDbOfficer> GetCommonDbOfficers()
        {
            var mayorsText = ReadMayorsFile(Constants.JsonDataFileName);

            var mayorsCollection = DeserializeMayorCollection(mayorsText);

            return MapMayorsToCommonDbOfficer(mayorsCollection);
        }

        public IEnumerable<CommonDbOfficer> MapMayorsToCommonDbOfficer(IEnumerable<DbMayor> mayors)
        {
            foreach (var mayor in mayors)
            {
                var dbCommonOfficer = new CommonDbOfficer();
                dbCommonOfficer.FirstName = mayor.FirstName;
                dbCommonOfficer.LastName = mayor.LastName;
                dbCommonOfficer.TookOffice = mayor.TakeOffice;
                dbCommonOfficer.LeftOffice = mayor.LeftOffice;
                dbCommonOfficer.Party = string.Empty;
                dbCommonOfficer.DataType = "M";

                yield return dbCommonOfficer;
            }
        }

        public IEnumerable<DbMayor> DeserializeMayorCollection(string mayorText)
        {
            var mayorsCollection = JsonConvert.DeserializeObject<IEnumerable<DbMayor>>(mayorText);

            return mayorsCollection;
        }

        public string ReadMayorsFile(string path)
        {
            string mayors = File.ReadAllText(path);

            int index1 = mayors.IndexOf('[');

            int index2 = mayors.IndexOf(']') - index1 + 1;

            mayors = mayors.Substring(index1, index2);

            return mayors;
        }
    }
}