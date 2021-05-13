using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairCompanyManagement.DataAccess.Repositories
{
    class FeedbackRepository : IRepository<Feedback>
    {
        private readonly string connectionString;

        public FeedbackRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Create(Feedback item)
        {
            string sqlExpression = $"INSERT INTO Feedback (IdOrder, Review)" +
                " VALUES (@idOrder, @review); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@idOrder", item.IdOrder),
                            new SqlParameter("@review", item.Review),
                        });

                    return command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Feedback WHERE Id=@id";

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

        public IEnumerable<Feedback> GetAll()
        {
            string sqlExpression = "SELECT Id, IdOrder, Review FROM Feedback";
            List<Feedback> feedbackControllers = new List<Feedback>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            feedbackControllers.Add(new Feedback()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                IdOrder = (int)reader["IdOrder"],
                                Review = reader["Review"].ToString(),
                            });
                        }
                    }
                }
            }

            return feedbackControllers;
        }

        public Feedback GetById(int id)
        {
            string sqlExpression = "SELECT Id, IdOrder, Review FROM Feedback" +
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
                        return reader.Read() ? new Feedback()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            IdOrder = (int)reader["IdOrder"],
                            Review = reader["Review"].ToString()
                        } : null;
                    }
                }
            }
        }

        public void Update(Feedback item)
        {
            string sqlExpression = "UPDATE JobPosition SET IdOrder=@idOrder, Review=@review" +
                " FROM Feedback" +
                " WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@id", item.Id),
                            new SqlParameter("@idOrder", item.IdOrder),
                            new SqlParameter("@review", item.Review)
                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
