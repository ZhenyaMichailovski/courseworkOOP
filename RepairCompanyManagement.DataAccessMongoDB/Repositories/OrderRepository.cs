using RepairCompanyManagement.DataAccessMongoDB.Interfaces;
using RepairCompanyManagement.DataAccessMongoDB.Entities;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RepairCompanyManagement.DataAccessMongoDB.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private string connectionString;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        public OrderRepository()
        {
            connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("RepairCompanyManagment");
            collection = database.GetCollection<BsonDocument>("Order");
        }
        public List<Order> GetAll()
        {
            List<Order> order = new List<Order>();
            using (var cursor = collection.Find(new BsonDocument()).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        var customer = doc.AsBsonDocument["Customer"];
                        
                        List<OrderTask> items = new List<OrderTask>();
                        
                        order.Add(new Order()
                        {
                            Title = doc["Title"].AsString,
                            Customer = new Customer()
                            {
                                Id = doc["id"].AsInt32,
                                Surname = doc["Surname"].AsString,
                                FirstName = doc["FirstName"].AsString,
                                LastName = doc["LastName"].AsString,
                                Email = doc["Email"].AsString
                            },
                            Requirements = doc["Requirements"].AsString,
                            Review = doc["Review"].AsString
                        });
                    }
                }
            }
            return brigade;
        }

        public void Create(Brigade brigade)
        {
            var items = GetAll();
            BsonDocument elements = new BsonDocument
            {
                { "id", new BsonInt32(items.Count() + 1) },
                { "Title", brigade.Title },
                { "Specialization", new BsonDocument {
                    { "id", new BsonInt32(brigade.Specialization.Id) },
                    { "Name", brigade.Specialization.Name }
                    }
                }
            };

            collection.InsertOne(elements);

        }
        public Brigade GetById(int id)
        {
            var filter = new BsonDocument("id", id);
            var doc = collection.Find(filter).FirstOrDefault();

            var spec = doc.AsBsonDocument["Specialization"];
            var specialization = new Brigade()
            {
                Id = doc["id"].AsInt32,
                Title = doc["Title"].AsString,
                Specialization = new Specialization()
                {
                    Id = spec["id"].AsInt32,
                    Name = spec["Name"].AsString,
                }
            };

            return specialization;
        }
        public void Delete(int id)
        {
            collection.DeleteOne(new BsonDocument { { "id", id } });
        }
    }
}
