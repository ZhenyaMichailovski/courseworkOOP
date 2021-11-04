using RepairCompanyManagement.DataAccessMongoDB.Interfaces;
using RepairCompanyManagement.DataAccessMongoDB.Entities;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RepairCompanyManagement.DataAccessMongoDB.Repositories
{
    public class BrigadeRepository : IRepository<Brigade>
    {
        private string connectionString;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        public BrigadeRepository()
        {
            connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("RepairCompanyManagment");
            collection = database.GetCollection<BsonDocument>("Brigade");
        }
        public List<Brigade> GetAll()
        {
            List<Brigade> brigade = new List<Brigade>();
            using (var cursor = collection.Find(new BsonDocument()).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        var spec = doc.AsBsonDocument["Specialization"];
                        brigade.Add(new Brigade()
                        {
                            Id = doc["id"].AsInt32,
                            Title = doc["Title"].AsString,
                            Specialization = new Specialization()
                            {
                                Id = spec["id"].AsInt32,
                                Name = spec["Name"].AsString,
                            }
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
