using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairCompanyManagement.DataAccess.Repositories
{
    class OrderTaskRepository : IRepository<OrderTask>
    {
        private readonly string connectionString;

        public OrderTaskRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Create(OrderTask item)
        {
            string sqlExpression = $"INSERT INTO OrderTask (IdTask, IdOrder, TaskCompletionDate)" +
                " VALUES (@idTask, @idOrder, @taskCompletionDate); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@idTask", item.IdTask),
                            new SqlParameter("@idOrder", item.IdOrder),
                            new SqlParameter("@taskCompletionDate", item.TaskCompletionDate),
                        });

                    return command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM OrderTask WHERE Id=@id";

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

        public IEnumerable<OrderTask> GetAll()
        {
            string sqlExpression = "SELECT Id, IdTask, IdOrder, TaskCompletionDate FROM OrderTask";
            List<OrderTask> orderTask = new List<OrderTask>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderTask.Add(new OrderTask()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                IdTask = (int)reader["IdTask"],
                                IdOrder = (int)reader["IdOrder"],
                                TaskCompletionDate = DateTimeOffset.Parse(reader["TaskCompletionDate"].ToString())
                            });
                        }
                    }
                }
            }

            return orderTask;
        }

        public OrderTask GetById(int id)
        {
            string sqlExpression = "SELECT Id, IdTask, IdOrder, TaskCompletionDate FROM OrderTask" +
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
                        return reader.Read() ? new OrderTask()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            IdTask = (int)reader["IdTask"],
                            IdOrder = (int)reader["IdOrder"],
                            TaskCompletionDate = DateTimeOffset.Parse(reader["TaskCompletionDate"].ToString())
                        } : null;
                    }
                }
            }
        }

        public void Update(OrderTask item)
        {
            string sqlExpression = "UPDATE OrderTask SET IdTask=@idTask, IdOrder=@idOrder, TaskCompletionDate=@taskCompletionDate" +
                " FROM OrderTask" +
                " WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@id", item.Id),
                            new SqlParameter("@IdTask", item.IdTask),
                            new SqlParameter("@IdOrder", item.IdOrder),
                            new SqlParameter("@taskCompletionDate", item.TaskCompletionDate),
                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
