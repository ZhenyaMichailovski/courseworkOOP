using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairCompanyManagement.DataAccess.Repositories
{
    class CustomerRepository : IRepository<Customer>
    {
        private readonly string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Create(Customer item)
        {
            string sqlExpression = $"INSERT INTO Customer (Gender, IdentityUserID)" +
                " VALUES (@gender, @identityUserID); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@idTask", item.Gender),
                            new SqlParameter("@idOrder", item.IdentityUserID),

                        });

                    return command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Customer WHERE Id=@id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    SqlParameter idParam = new SqlParameter("@id", id);
                    command.Parameters.Add(idParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            string sqlExpression = "SELECT Id, Gender, IdentityUserID FROM Customer";
            List<Customer> customer = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer.Add(new Customer()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                Gender = (string)reader["Gender"],
                                IdentityUserID = (string)reader["IdentityUserID"],
                            });
                        }
                    }
                }
            }

            return customer;
        }

        public Customer GetById(int id)
        {
            string sqlExpression = "SELECT Id, Gender, IdentityUserID FROM Customer" +
                " WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    SqlParameter idParam = new SqlParameter(@"id", id);
                    command.Parameters.Add(idParam);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read() ? new Customer()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            Gender = (string)reader["Gender"],
                            IdentityUserID = (string)reader["IdentityUserID"],
                        } : null;
                    }
                }
            }
        }

        public void Update(Customer item)
        {
            string sqlExpression = "UPDATE Customer SET Gender=@gender, IdentityUserID=@identityUserID" +
                " FROM Customer" +
                " WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@gender", item.Gender),
                            new SqlParameter("@identityUserID", item.IdentityUserID),
                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
