namespace APUS.DataAccess
{
    using APUS.DataAccess.DbModels;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public class JsonDataAccess : IDataAccess
    {
        public IEnumerable<DbPresident> GetDbPresidents()
        {
            var mayorsText = ReadMayorsFile(Constants.JsonDataFileName);

            var mayorsCollection = DeserializeMayorCollection(mayorsText);

            return MapMayorsToDbPresident(mayorsCollection);
        }

        public IEnumerable<DbPresident> MapMayorsToDbPresident(IEnumerable<DbMayor> mayors)
        {
            foreach (var mayor in mayors)
            {
                var dbPresident = new DbPresident();
                dbPresident.FirstName = mayor.FirstName;
                dbPresident.LastName = mayor.LastName;
                dbPresident.TookOffice = mayor.TakeOffice;
                dbPresident.LeftOffice = mayor.LeftOffice;
                dbPresident.Party = string.Empty;

                yield return dbPresident;
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