using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairCompanyManagement.DataAccess.Repositories
{
    public class JobPositionRepository : IRepository<JobPosition>
    {
        private readonly string connectionString;

        public JobPositionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Create(JobPosition item)
        {
            string sqlExpression = $"INSERT INTO JobPosistionController (Title, Purpose)" +
                " VALUES (@title, @purpose); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@title", item.Title),
                            new SqlParameter("@purpose", item.Purpose),
                        });

                    return command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM JobPosistionController WHERE Id=@id";

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

        public IEnumerable<JobPosition> GetAll()
        {
            string sqlExpression = "SELECT Id, Title, Purpose FROM JobPisitionController";
            List<JobPosition> jobPisitionControllers = new List<JobPosition>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobPisitionControllers.Add(new JobPosition()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                Title = reader["Title"].ToString(),
                                Purpose = reader["Purpose"].ToString(),
                            });
                        }
                    }
                }
            }

            return jobPisitionControllers;
        }
        
        public JobPosition GetById(int id)
        {
            string sqlExpression = "SELECT Id, Title, Purpose FROM JobPisitionController" +
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
                        return reader.Read() ? new JobPosition()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            Title = reader["Title"].ToString(),
                            Purpose = reader["Purpose"].ToString()
                        } : null;
                    }
                }
            }
        }

        public void Update(JobPosition item)
        {
            string sqlExpression = "UPDATE JobPisitionController SET Title=@title, Purpose=@purpose" +
                " FROM JobPisitionController" +
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
                            new SqlParameter("@purpose", item.Purpose)
                        });
                }
            }
        }
    }
}
