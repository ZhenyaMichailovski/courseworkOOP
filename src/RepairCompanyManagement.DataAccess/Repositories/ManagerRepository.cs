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
            string sqlExpression = $"INSERT INTO Manager (DateOfBirth, Address, Salary, IdentituUserID)" +
                " VALUES (@dateOfBirth, @address, @salary, @identituUserID); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@dateOfBirth", item.DateOfBirth),
                            new SqlParameter("@address", item.Address),
                            new SqlParameter("@salary", item.Salary),
                            new SqlParameter("@identituUserID", item.IdentituUserID),
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
            string sqlExpression = "SELECT Id, DateOfBirth, Address, Salary, IdentituUser FROM Manager";
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
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Address = reader["Address"].ToString(),
                                Salary = Convert.ToDouble(reader["Salary"]),
                                IdentituUserID = reader["IdentituUser"].ToString(),
                            });
                        }
                    }
                }
            }

            return manager;
        }

        public Manager GetById(int id)
        {
            string sqlExpression = "SELECT Id, DateOfBirth, Address, Salary, IdentituUser FROM Manager" +
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
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Address = reader["Address"].ToString(),
                            Salary = Convert.ToDouble(reader["Salary"]),
                            IdentituUserID = reader["IdentituUser"].ToString(),
                        } : null;
                    }
                }
            }
        }

        public void Update(Manager item)
        {
            string sqlExpression = "UPDATE Manager SET Manager=@manager, Address=@address, Salary=@salary, IdentituUser=@identituUser" +
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
                            new SqlParameter("@dateOfBirth", item.DateOfBirth),
                            new SqlParameter("@address", item.Address),
                            new SqlParameter("@salary", item.Salary),
                            new SqlParameter("@identituUserID", item.IdentituUserID),
                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
