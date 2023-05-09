using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Entities;

namespace ExtensionTools.DLL
{
    public class MongoDBHelp
    {
        public IMongoClient _client = null;
        public IMongoDatabase _database = null;
        public Task MongoEntities;

        public async Task MongoDBHelpAsync(string con, string database)
        {
            await DB.InitAsync(database, MongoClientSettings.FromConnectionString(con));//"mongodb+srv://user:password@cluster.mongodb.net/DatabaseName"
        }

        public MongoDBHelp(string con, string database)
        {

            MongoEntities = MongoDBHelpAsync(con, database);

            // _client = MongoEntities.
        }




















































    }
}
