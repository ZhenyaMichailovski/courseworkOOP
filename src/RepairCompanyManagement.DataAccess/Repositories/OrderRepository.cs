using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairCompanyManagement.DataAccess.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly string connectionString;

        public OrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Create(Order item)
        {
            string sqlExpression = $"INSERT INTO Order (Title, IdBrigade, IdCustomers, IdManager, OrderStatus, Requirements)" +
                " VALUES (@title, @idBrigade, @idCustomers, @idManager, @idTask, @orderStatus, @requirements); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@title", item.Title),
                            new SqlParameter("@idBrigade", item.IdBrigade),
                            new SqlParameter("@idCustomers", item.IdCustomers),
                            new SqlParameter("@idManager", item.IdManager),
                            
                            new SqlParameter("@orderStatus", item.OrderStatus),
                            new SqlParameter("@requirements", item.Requirements),
                        });

                    return command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Order WHERE Id=@id";

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

        public IEnumerable<Order> GetAll()
        {
            string sqlExpression = "SELECT Id, Title, IdBrigade, IdCustomers, IdManager, OrderStatus, Requirements FROM Order";
            List<Order> order = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            order.Add(new Order()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                Title = (string)reader["Title"],
                                IdBrigade = (int)reader["IdBrigade"],
                                IdCustomers = (int)(reader["IdCustomers"]),
                                IdManager = (int)reader["IdManager"],
                          
                                OrderStatus = reader["OrderStatus"].ToString(),
                                Requirements = reader["Requirements"].ToString(),
                            });
                        }
                    }
                }
            }

            return order;
        }

        public Order GetById(int id)
        {
            string sqlExpression = "SELECT Id, Title, IdBrigade, IdCustomers, IdManager, OrderStatus, Requirements FROM Order" +
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
                        return reader.Read() ? new Order()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            Title = (string)reader["Title"],
                            IdBrigade = (int)reader["IdBrigade"],
                            IdCustomers = (int)(reader["IdCustomers"]),
                            IdManager = (int)reader["IdManager"],
                            
                            OrderStatus = reader["OrderStatus"].ToString(),
                            Requirements = reader["Requirements"].ToString(),
                        } : null;
                    }
                }
            }
        }

        public void Update(Order item)
        {
            string sqlExpression = "UPDATE Manager SET Title=@title, IdBrigade=@idBrigade, IdCustomers=@idCustomers, IdManager=@idManager, OrderStatus=@orderStatus, Requirements=@requirements " +
                " FROM Order" +
                " WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@id", item.Id),
                            new SqlParameter("@title", item.Title),
                            new SqlParameter("@idBrigade", item.IdBrigade),
                            new SqlParameter("@idCustomers", item.IdCustomers),
                            new SqlParameter("@idManager", item.IdManager),
                      
                            new SqlParameter("@orderStatus", item.OrderStatus),
                            new SqlParameter("@requirements", item.Requirements),
                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
