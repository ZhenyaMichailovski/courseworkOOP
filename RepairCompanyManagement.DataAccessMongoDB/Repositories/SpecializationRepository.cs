using RepairCompanyManagement.DataAccessMongoDB.Interfaces;
using RepairCompanyManagement.DataAccessMongoDB.Entities;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RepairCompanyManagement.DataAccessMongoDB.Repositories
{
    public class SpecializationRepository : IRepository<Specialization>
    {
        private string connectionString;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        public SpecializationRepository()
        {
            connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("RepairCompanyManagment");
            collection = database.GetCollection<BsonDocument>("Specialization");
        }
        public List<Specialization> GetAll()
        {
            List<Specialization> specializations = new List<Specialization>();
            using (var cursor = collection.Find(new BsonDocument()).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        specializations.Add(new Specialization()
                        {
                            Id = doc["id"].AsInt32,
                            Description = doc["Description"].AsString,
                            Name = doc["Name"].AsString
                        });
                    }
                }
            }
            return specializations;
        }

        public void Create(Specialization specialization)
        {
            var items = GetAll();
            BsonDocument elements = new BsonDocument
            {
                { "id", new BsonInt32(items.Count() + 1) },
                { "Name", specialization.Name },
                { "Description", specialization.Description}
            };

            collection.InsertOne(elements);
           
        }
        public Specialization GetById(int id)
        {
            var filter = new BsonDocument("id", id);
            var doc = collection.Find(filter).FirstOrDefault();

            var specialization = new Specialization()
            {
                Id = doc["id"].AsInt32,
                Description = doc["Description"].AsString,
                Name = doc["Name"].AsString
            };

            return specialization;
        }
        public void Delete(int id)
        {
            collection.DeleteOne(new BsonDocument{ { "id", id } });
        }
    }
}
