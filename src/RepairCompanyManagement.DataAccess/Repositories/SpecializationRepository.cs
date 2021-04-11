using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairCompanyManagement.DataAccess.Repositories
{
    public class SpecializationRepository : IRepository<Specialization>
    {
        private readonly string connectionString;

        public SpecializationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Create(Specialization item)
        {
            string sqlExpression = $"INSERT INTO Specialization (Name, Description)" +
                " VALUES (@name, @description); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@name", item.Name),
                        new SqlParameter("@description", item.Description),
                    });

                    return command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Specialization WHERE Id=@id";

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

        public IEnumerable<Specialization> GetAll()
        {
            string sqlExpression = "SELECT Id, Name, Description FROM Specialization";
            List<Specialization> specializations = new List<Specialization>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            specializations.Add(new Specialization()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                            });
                        }
                    }
                }
            }

            return specializations;
        }

        public Specialization GetById(int id)
        {
            string sqlExpression = "SELECT Id, Name, Description FROM Specialization" +
                " WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    SqlParameter idParam = new SqlParameter("@id", id);
                    command.Parameters.Add(idParam);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read() ? new Specialization()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                        } : null;
                    }
                }
            }
        }

        public void Update(Specialization item)
        {
            string sqlExpression = "UPDATE Specialization SET Name=@name, Description=@description" +
                " FROM Specialization" +
                " WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@id", item.Id),
                            new SqlParameter("@name", item.Name),
                            new SqlParameter("@description", item.Description),
                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
