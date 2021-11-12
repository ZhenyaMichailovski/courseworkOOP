using RepairCompanyManagement.DataAccessMongoDB.Interfaces;
using RepairCompanyManagement.DataAccessMongoDB.Entities;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System;

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
                        var tmp = doc["OrderTask"].AsBsonArray;
                        foreach(var i in tmp)
                        {
                            items.Add(new OrderTask()
                            {
                                IdTask = i["IdTask"].AsInt32,
                                Status = i["Statuc"].AsString,
                                Decsription = i["Decsription"].AsString,
                                TaskCompletionDate = Convert.ToDateTime(i["TaskCompletionDate"].AsString)
                            });
                        }
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
                            OrderTask = items,
                            Requirements = doc["Requirements"].AsString,
                            Review = doc["Review"].AsString
                        });
                    }
                }
            }
            return order;
        }

        public void Create(Order order)
        {
            var items = GetAll();
            BsonArray array = new BsonArray();
            
            for(int i = 0; i < order.OrderTask.Count; i++)
            {
                array.Add(new BsonDocument
                {
                    
                    { "IdTask", new BsonInt32(order.OrderTask[i].IdTask) },
                    { "Statuc", new BsonString(order.OrderTask[i].Status) },
                    { "Decsription", new BsonString(order.OrderTask[i].Decsription) },
                    { "TaskCompletionDate", new BsonDateTime(order.OrderTask[i].TaskCompletionDate) }
                });
            }
            BsonDocument elements = new BsonDocument
            {
                { "Title", order.Title },
                { "Customer", new BsonDocument{
                    { "Id", new BsonInt32(order.Customer.Id) },
                    { "Surname", new BsonString(order.Customer.Surname) },
                    { "FirstName", new BsonString(order.Customer.FirstName) },
                    { "LastName", new BsonString(order.Customer.LastName) },
                    { "Email", new BsonString(order.Customer.Email) },
                } },
                { "Requirements", new BsonString(order.Requirements)},
                { "Review", new BsonString(order.Review) },
                { "OrderTask", new BsonArray(array) }
            };

            collection.InsertOne(elements);

        }
        public Order GetById(int id)
        {
            var filter = new BsonDocument("id", id);
            var doc = collection.Find(filter).FirstOrDefault();
            List<OrderTask> items = new List<OrderTask>();
            var tmp = doc["OrderTask"].AsBsonArray;
            
            foreach (var i in tmp)
            {
                items.Add(new OrderTask()
                {
                    IdTask = i["IdTask"].AsInt32,
                    Status = i["Statuc"].AsString,
                    Decsription = i["Decsription"].AsString,
                    TaskCompletionDate = Convert.ToDateTime(i["TaskCompletionDate"].AsString)
                });
            }
            var order = new Order()
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
                OrderTask = items,
                Requirements = doc["Requirements"].AsString,
                Review = doc["Review"].AsString
            };

            return order;
        }
        public void Delete(int id)
        {
            collection.DeleteOne(new BsonDocument { { "id", id } });
        }
    }
}
