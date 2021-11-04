using RepairCompanyManagement.DataAccessMongoDB.Interfaces;
using RepairCompanyManagement.DataAccessMongoDB.Entities;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RepairCompanyManagement.DataAccessMongoDB.Repositories
{
    public class ManagerRepository : IRepository<Manager>
    {
        private string connectionString;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        public ManagerRepository()
        {
            connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("RepairCompanyManagment");
            collection = database.GetCollection<BsonDocument>("Manager");
        }
        public List<Manager> GetAll()
        {
            List<Manager> manager = new List<Manager>();
            using (var cursor = collection.Find(new BsonDocument()).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        manager.Add(new Manager()
                        {
                            Id = doc["id"].AsInt32,
                            Surname = doc["Surname"].AsString,
                            FirstName = doc["FirstName"].AsString,
                            LastName = doc["LastName"].AsString,
                            Email = doc["Email"].AsString,
                            Salary = doc["Salary"].AsDecimal
                        });
                    }
                }
            }
            return manager;
        }

        public void Create(Manager customer)
        {
            var items = GetAll();
            BsonDocument elements = new BsonDocument
            {
                { "id", new BsonInt32(items.Count() + 1) },
                { "Surname", customer.Surname },
                { "FirstName", customer.FirstName },
                { "LastName", customer.LastName },
                { "Email", customer.Email },
                { "Salary", new BsonDecimal128(customer.Salary) }

            };

            collection.InsertOne(elements);

        }
        public Manager GetById(int id)
        {
            var filter = new BsonDocument("id", id);
            var doc = collection.Find(filter).FirstOrDefault();

            var manager = new Manager()
            {
                Id = doc["id"].AsInt32,
                Surname = doc["Surname"].AsString,
                FirstName = doc["FirstName"].AsString,
                LastName = doc["LastName"].AsString,
                Email = doc["Email"].AsString,
                Salary = doc["Salaey"].AsDecimal

            };

            return manager;
        }
        public void Delete(int id)
        {
            collection.DeleteOne(new BsonDocument { { "id", id } });
        }
    }
}
