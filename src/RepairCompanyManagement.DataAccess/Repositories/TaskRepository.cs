﻿using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Enums;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairCompanyManagement.DataAccess.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly string connectionString;

        public TaskRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Create(Task item)
        {
            string sqlExpression = $"INSERT INTO Task (Title, IdSpecialization, Price, Description, TaskCompletionDate, IdBrigade, Status)" +
                " VALUES (@title, @idSpecialization, @price, @description, @taskCompletionDate, @idBrigade); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@title", item.Title),
                            new SqlParameter("@idSpecialization", item.IdSpecialization),
                            new SqlParameter("@price", item.Price),
                            new SqlParameter("@description", item.Description),
                            new SqlParameter("@taskCompletionDate", item.TaskCompletionDate),
                            new SqlParameter("@idBrigade", item.IdBrigade),
                            new SqlParameter("@status", item.Status),
                        });

                    return command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Task WHERE Id=@id";

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

        public IEnumerable<Task> GetAll()
        {
            string sqlExpression = "SELECT Id, Title, IdSpecialization, Price, Description, TaskCompletionDate, IdBrigade, Status FROM Task";
            List<Task> task = new List<Task>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            task.Add(new Task()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                Title = (string)reader["Title"],
                                IdSpecialization = (int)reader["IdSpecialization"],
                                Price = Convert.ToDecimal(reader["Price"]),
                                Description = reader["Description"].ToString(),
                                TaskCompletionDate = Convert.ToDateTime(reader["TaskCompletionDate"]),
                                IdBrigade = (int)reader["IdBrigade"],
                                Status = (Enums.TaskStatus)int.Parse(reader["Status"].ToString()),
                            }) ;
                        }
                    }
                }
            }

            return task;
        }

        public Task GetById(int id)
        {
            string sqlExpression = "SELECT Id, Title, IdSpecialization, Price, Description, TaskCompletionDate, IdBrigade, Status FROM Task" +
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
                        return reader.Read() ? new Task()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            Title = (string)reader["Title"],
                            IdSpecialization = (int)reader["IdSpecialization"],
                            Price = Convert.ToDecimal(reader["Price"]),
                            Description = reader["Description"].ToString(),
                            TaskCompletionDate = Convert.ToDateTime(reader["TaskCompletionDate"]),
                            IdBrigade = (int)reader["IdBrigade"],
                            Status = (Enums.TaskStatus)int.Parse(reader["Status"].ToString())
                        } : null;
                    }
                }
            }
        }

        public void Update(Task item)
        {
            string sqlExpression = "UPDATE Task SET Title=@title, IdSpecialization=@idSpecialization, Price=@price, Description=@description, TaskCompletionDate=@taskCompletionDate, IdBrigade=@idBrigade, Status=@status" +
                " FROM Task" +
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
                            new SqlParameter("@idSpecialization", item.IdSpecialization),
                            new SqlParameter("@price", item.Price),
                            new SqlParameter("@description", item.Description),
                            new SqlParameter("@taskCompletionDate", item.TaskCompletionDate),
                            new SqlParameter("@idBrigade", item.IdBrigade),
                            new SqlParameter("@status", item.Status)
                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
