using RepairCompanyManagement.DataAccessMongoDB.Interfaces;
using RepairCompanyManagement.DataAccessMongoDB.Entities;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RepairCompanyManagement.DataAccessMongoDB.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private string connectionString;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        public CustomerRepository()
        {
            connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("RepairCompanyManagment");
            collection = database.GetCollection<BsonDocument>("Customer");
        }
        public List<Customer> GetAll()
        {
            List<Customer> customer = new List<Customer>();
            using (var cursor = collection.Find(new BsonDocument()).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        customer.Add(new Customer()
                        {
                            Id = doc["id"].AsInt32,
                            Surname = doc["Surname"].AsString,
                            FirstName = doc["FirstName"].AsString,
                            LastName = doc["LastName"].AsString,
                            Email = doc["Email"].AsString,
                            Balance = doc["Balance"].AsDecimal
                        });
                    }
                }
            }
            return customer;
        }

        public void Create(Customer customer)
        {
            var items = GetAll();
            BsonDocument elements = new BsonDocument
            {
                { "id", new BsonInt32(items.Count() + 1) },
                { "Surname", customer.Surname },
                { "FirstName", customer.FirstName },
                { "LastName", customer.LastName },
                { "Email", customer.Email },
                { "Balance", new BsonDecimal128(customer.Balance) }
               
            };

            collection.InsertOne(elements);

        }
        public Customer GetById(int id)
        {
            var filter = new BsonDocument("id", id);
            var doc = collection.Find(filter).FirstOrDefault();

            var customer = new Customer()
            {
                Id = doc["id"].AsInt32,
                Surname = doc["Surname"].AsString,
                FirstName = doc["FirstName"].AsString,
                LastName = doc["LastName"].AsString,
                Email = doc["Email"].AsString,
                Balance = doc["Balance"].AsDecimal

            };

            return customer;
        }
        public void Delete(int id)
        {
            collection.DeleteOne(new BsonDocument { { "id", id } });
        }
    }
}
