using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairCompanyManagement.DataAccess.Repositories
{
    class EmployeeRepository : IRepository<Employee>
    {
        private readonly string connectionString;

        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Create(Employee item)
        {
            string sqlExpression = $"INSERT INTO Employee (IdBrigade, Salary, IdJobPosition, IdentityUserID)" +
                " VALUES (@idBrigade, @salary, @idJobPosition, @identityUserID); SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@idBrigade", item.IdBrigade),
                            new SqlParameter("@salary", item.Salary),
                            new SqlParameter("@idJobPosition", item.IdJobPosition),
                            new SqlParameter("@identityUserID", item.IdentityUserID),
                           
                        });

                    return command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Employee WHERE Id=@id";

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

        public IEnumerable<Employee> GetAll()
        {
            string sqlExpression = "SELECT Id, IdBrigade, Salary, IdJobPosition, IdentityUserID FROM Employee";
            List<Employee> employee = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee.Add(new Employee()
                            {
                                Id = Convert.ToInt32(reader["Id"], null),
                                IdBrigade = (int)reader["IdBrigade"],
                                Salary = (decimal)reader["Salary"],
                                IdJobPosition = (int)(reader["IdJobPosition"]),
                                IdentityUserID = (string)reader["IdentityUserID"],
                               
                            });
                        }
                    }
                }
            }

            return employee;
        }

        public Employee GetById(int id)
        {
            string sqlExpression = "SELECT Id, IdBrigade, Salary, IdJobPosition, IdentityUserID FROM Employee" +
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
                        return reader.Read() ? new Employee()
                        {
                            Id = Convert.ToInt32(reader["Id"], null),
                            IdBrigade = (int)reader["IdBrigade"],
                            Salary = (decimal)reader["Salary"],
                            IdJobPosition = (int)(reader["IdJobPosition"]),
                            IdentityUserID = (string)reader["IdentityUserID"],
                        } : null;
                    }
                }
            }
        }

        public void Update(Employee item)
        {
            string sqlExpression = "UPDATE Employee SET IdBrigade=@idBrigade, Salary=@salary, IdJobPosition=@idJobPosition, IdentityUserID=@identityUserID" +
                " FROM Employee" +
                " WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@id", item.Id),
                            new SqlParameter("@idBrigade", item.IdBrigade),
                            new SqlParameter("@salary", item.Salary),
                            new SqlParameter("@idJobPosition", item.IdJobPosition),
                            new SqlParameter("@identityUserID", item.IdentityUserID),

                        });

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
