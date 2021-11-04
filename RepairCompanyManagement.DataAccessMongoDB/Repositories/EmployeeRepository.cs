using RepairCompanyManagement.DataAccessMongoDB.Interfaces;
using RepairCompanyManagement.DataAccessMongoDB.Entities;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RepairCompanyManagement.DataAccessMongoDB.Repositories
{
    class EmployeeRepository : IRepository<Employee>
    {
        private string connectionString;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        public EmployeeRepository()
        {
            connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("RepairCompanyManagment");
            collection = database.GetCollection<BsonDocument>("Employee");
        }
        public List<Employee> GetAll()
        {
            List<Employee> employee = new List<Employee>();
            using (var cursor = collection.Find(new BsonDocument()).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        var brigade = doc.AsBsonDocument["Brigade"];
                        employee.Add(new Employee()
                        {
                            Id = doc["id"].AsInt32,
                            Surname = doc["Surname"].AsString,
                            FirstName = doc["FirstName"].AsString,
                            LastName = doc["LastName"].AsString,
                            Email = doc["email"].AsString,
                            Salary = doc["Salary"].AsDecimal,
                            Brigade = new Brigade() 
                            {
                                Id = brigade["id"].AsInt32,
                                Title = brigade["Title"].AsString
                            }
                        });
                    }
                }
            }
            return employee;
        }

        public void Create(Employee employee)
        {
            var items = GetAll();
            BsonDocument elements = new BsonDocument
            {
                { "id", new BsonInt32(items.Count() + 1) },
                { "Surname", employee.Surname },
                { "FirstName", employee.FirstName },
                { "LastName", employee.LastName },
                { "email", employee.Email },
                { "Salary", new BsonDecimal128(employee.Salary) },
                { "Brigade", new BsonDocument {
                    { "id", new BsonInt32(employee.Brigade.Id) },
                    { "Title", employee.Brigade.Title }
                    }
                }
            };

            collection.InsertOne(elements);

        }
        public Employee GetById(int id)
        {
            var filter = new BsonDocument("id", id);
            var doc = collection.Find(filter).FirstOrDefault();

            var brigade = doc.AsBsonDocument["Brigade"];
            var employee = new Employee()
            {
                Id = doc["id"].AsInt32,
                Surname = doc["Surname"].AsString,
                FirstName = doc["FirstName"].AsString,
                LastName = doc["LastName"].AsString,
                Email = doc["email"].AsString,
                Salary = doc["Salary"].AsDecimal,
                Brigade = new Brigade()
                {
                    Id = brigade["id"].AsInt32,
                    Title = brigade["Title"].AsString
                }
            };

            return employee;
        }
        public void Delete(int id)
        {
            collection.DeleteOne(new BsonDocument { { "id", id } });
        }
    }
}
