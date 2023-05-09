using System;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Entities;

using PublicTools;

namespace GlobalBase.MongoModel;

public class MongoCommon
{
    static string DataBaseName = GetConfig.GetConfigs("MongoDBConnection:DataBaseName");

    static MongoClientSettings settings = new MongoClientSettings()
    {
        Server = new MongoServerAddress(GetConfig.GetConfigs("MongoDBConnection:localhost"),
            Convert.ToInt32(GetConfig.GetConfigs("MongoDBConnection:port"))),
        Credential = MongoCredential.CreateCredential(DataBaseName,
            GetConfig.GetConfigs("MongoDBConnection:user"),
            GetConfig.GetConfigs("MongoDBConnection:pwd")),
        MaxConnectionPoolSize = int.Parse(GetConfig.GetConfigs("MongoDBConnection:MaxConnection"))
    };

    private static bool b;

    public static async Task Init()
    {
        if (!b) await DB.InitAsync(DataBaseName, settings);
        else b = false;
    }

    /// <summary>
    /// 初始化MongoDB.driver
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns>IMongoCollection：T </returns>
    public static async Task<IMongoDatabase> InitDriver()
    {
        MongoClient mongoClient = new MongoClient(settings);
        var database = mongoClient.GetDatabase(DataBaseName);
        return database;
    }
}

