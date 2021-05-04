using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace RepairCompanyManagement.DataAccess.Repositories
{
    public class ManagerRepository : IRepository<Manager>
    {
        private readonly string connectionString;

        public ManagerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Create(Manager item)
        {
            string sqlExpression = $"INSERT INTO Manager (Salary, IdentityUserID)" +
                " VALUES (@salary, @IdentityUserID); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@salary", item.Salary),
                            new SqlParameter("@IdentityUserID", item.IdentityUserID),
                        });

                    return command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Manager WHERE Id=@id";

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

        public IEnumerable<Manager> GetAll()
        {
            string sqlExpression = "SELECT Id, Salary, IdentityUserID FROM Manager";
            List<Manager> manager = new List<Manager>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            manager.Add(new Manager()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                Salary = Convert.ToDecimal(reader["Salary"]),
                                IdentityUserID = reader["IdentityUserID"].ToString(),
                            });
                        }
                    }
                }
            }

            return manager;
        }

        public Manager GetById(int id)
        {
            string sqlExpression = "SELECT Id, Salary, IdentituUser FROM Manager" +
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
                        return reader.Read() ? new Manager()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            Salary = Convert.ToDecimal(reader["Salary"]),
                            IdentityUserID = reader["IdentituUser"].ToString(),
                        } : null;
                    }
                }
            }
        }

        public void Update(Manager item)
        {
            string sqlExpression = "UPDATE Manager SET Salary=@salary, IdentituUser=@identituUser" +
                " FROM Manager" +
                " WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@id", item.Id),
                            new SqlParameter("@salary", item.Salary),
                            new SqlParameter("@IdentityUserID", item.IdentityUserID),
                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
