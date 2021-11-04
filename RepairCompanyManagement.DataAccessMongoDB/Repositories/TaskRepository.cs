using RepairCompanyManagement.DataAccessMongoDB.Interfaces;
using RepairCompanyManagement.DataAccessMongoDB.Entities;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RepairCompanyManagement.DataAccessMongoDB.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private string connectionString;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        public TaskRepository()
        {
            connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("RepairCompanyManagment");
            collection = database.GetCollection<BsonDocument>("Tasks");
        }
        public List<Task> GetAll()
        {
            List<Task> task = new List<Task>();
            using (var cursor = collection.Find(new BsonDocument()).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        var spec = doc.AsBsonDocument["Specialization"];
                        var brigade = doc.AsBsonDocument["Brigade"];
                        task.Add(new Task()
                        {
                            Id = doc["id"].AsInt32,
                            Title = doc["Title"].AsString,
                            Price = doc["Price"].AsDecimal,
                            Description = doc["Description"].AsString,
                            Specialization = new Specialization()
                            {
                                Id = spec["id"].AsInt32,
                                Name = spec["Name"].AsString,
                            },
                            Brigade = new Brigade()
                            {
                                Id = brigade["id"].AsInt32,
                                Title = brigade["Title"].AsString
                            }
                        });
                    }
                }
            }
            return task;
        }

        public void Create(Task task)
        {
            var items = GetAll();
            BsonDocument elements = new BsonDocument
            {
                { "id", new BsonInt32(items.Count() + 1) },
                { "Title", task.Title },
                { "Price", new BsonDecimal128(task.Price) },
                { "Description", task.Description },
                { "Specialization", new BsonDocument {
                    { "id", new BsonInt32(task.Specialization.Id) },
                    { "Name", task.Specialization.Name }
                    }
                },
                { "Brigade", new BsonDocument {
                    { "id", new BsonInt32(task.Brigade.Id) },
                    { "Title", task.Brigade.Title },
                }
                }
            };

            collection.InsertOne(elements);

        }
        public Task GetById(int id)
        {
            var filter = new BsonDocument("id", id);
            var doc = collection.Find(filter).FirstOrDefault();

            var spec = doc.AsBsonDocument["Specialization"];
            var brigade = doc.AsBsonDocument["Brigade"];
            var task = new Task()
            {
                Id = doc["id"].AsInt32,
                Title = doc["Title"].AsString,
                Price = doc["Price"].AsDecimal,
                Description = doc["Description"].AsString,
                Specialization = new Specialization()
                {
                    Id = spec["id"].AsInt32,
                    Name = spec["Name"].AsString,
                },
                Brigade = new Brigade()
                {
                    Id = brigade["id"].AsInt32,
                    Title = brigade["Title"].AsString
                }
            };

            return task;
        }
        public void Delete(int id)
        {
            collection.DeleteOne(new BsonDocument { { "id", id } });
        }
    }
}
