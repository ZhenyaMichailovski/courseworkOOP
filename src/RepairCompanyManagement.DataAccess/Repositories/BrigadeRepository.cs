using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairCompanyManagement.DataAccess.Repositories
{
    class BrigadeRepository : IRepository<Brigade>
    {
        private readonly string connectionString;

        public BrigadeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Create(Brigade item)
        {
            string sqlExpression = $"INSERT INTO Brigade (Title, IdSpecialization)" +
                " VALUES (@title, @idSpecialization); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@idTask", item.Title),
                            new SqlParameter("@idOrder", item.IdSpecialization),

                        });

                    return command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Brigade WHERE Id=@id";

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

        public IEnumerable<Brigade> GetAll()
        {
            string sqlExpression = "SELECT Id, Title, IdSpecialization FROM Brigade";
            List<Brigade> brigade = new List<Brigade>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            brigade.Add(new Brigade()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                Title = (string)reader["Title"],
                                IdSpecialization = (int)reader["IdSpecialization"],
                            });
                        }
                    }
                }
            }

            return brigade;
        }

        public Brigade GetById(int id)
        {
            string sqlExpression = "SELECT Id, Title, IdSpecialization FROM Brigade" +
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
                        return reader.Read() ? new Brigade()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            Title = (string)reader["Title"],
                            IdSpecialization = (int)reader["IdSpecialization"],
                        } : null;
                    }
                }
            }
        }

        public void Update(Brigade item)
        {
            string sqlExpression = "UPDATE Brigade SET Title=@title, IdSpecialization=@idSpecialization" +
                " FROM Brigade" +
                " WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@title", item.Title),
                            new SqlParameter("@idSpecialization", item.IdSpecialization),
                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
