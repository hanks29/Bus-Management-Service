using System;
using MongoDB.Driver;

namespace BusManagementService
{
    internal class MongoConnection
    {
        private MongoClient client;
        private IMongoDatabase database;

        public MongoConnection()
        {
            string connectionString = "mongodb://ur65yqjwdqm5t816sn7c:4HXzFS1JWo47JQUNZNse@bdlrqnificdbluqgocin-mongodb.services.clever-cloud.com:2475/bdlrqnificdbluqgocin";

            try
            {
                client = new MongoClient(connectionString);

                database = client.GetDatabase("bdlrqnificdbluqgocin");

                Console.WriteLine("Kết nối thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return database.GetCollection<T>(collectionName);
        }
    }
}