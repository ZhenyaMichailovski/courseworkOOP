using NUnit.Framework;
using RepairCompanyManagement.DataAccess.Repositories;
using RepairCompanyManagement.DataAccess.Entities;
using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagment.Tests
{
    [TestFixture]
    public class NUnitTests
    {
        private TaskRepository _taskRepository;

        [SetUp]
        public void SetUp()
        {
            _taskRepository = new TaskRepository(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RepairCompanyManagementDatabase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=RepairCompanyManagementDatabase;Trusted_Connection=True;");
        }

        [Test]
        public void GetTasks_WhenDatabaseIsCorrect_ShouldReturnCollectionOfTasks()
        {
            var items = _taskRepository.GetAll().ToList();

            items.Should().BeEquivalentTo(new List<RepairCompanyManagement.DataAccess.Entities.Task>
            {
                new RepairCompanyManagement.DataAccess.Entities.Task()
                {
                    Id = 1,
                    Title = "Electikst",
                    IdSpecialization = 1,
                    Price = 150,
                    Description = "hi-hi ha-ha",
                    IdBrigade = 1,
                }
            });
        }

        [Test]
        [TestCase(1)]
        public void GetTaskById_WhenTheItemIsInTheDatabase_ShouldReturnItem(int index)
        {
            var item = _taskRepository.GetById(index);

            item.Should().BeEquivalentTo(new RepairCompanyManagement.DataAccess.Entities.Task
            {
                Id = 1,
                Title = "Electikst",
                IdSpecialization = 1,
                Price = 150,
                Description = "hi-hi ha-ha",
                IdBrigade = 1,
            });
        }

        [Test]
        [TestCase(2)]
        [TestCase(-1)]
        [TestCase(null)]
        public void GetTaskById_WhenAnItemIsNotInTheDatabase_ShouldReturnException(int index)
        {
            var item = _taskRepository.GetById(index);

            item.Should().BeNull();
        }


        [Test]
        [TestCase(1)]
        public void CreateNewTask_WhenTheAllItemsIsCorrect_ShouldReturnNewCollectionOfTasks(int index)
        {
            var item = _taskRepository.Create(
                new RepairCompanyManagement.DataAccess.Entities.Task
                {
                    Id = 2,
                    Title = "Electikst1",
                    IdSpecialization = 1,
                    Price = 150,
                    Description = "hi-hi ha-ha",
                    IdBrigade = 1,
                });
            var items = _taskRepository.GetAll().ToList();
            _taskRepository.Delete(items[1].Id);
            items.Should().BeEquivalentTo(new List<RepairCompanyManagement.DataAccess.Entities.Task>
            {
                new RepairCompanyManagement.DataAccess.Entities.Task()
                {
                    Id = 1,
                    Title = "Electikst",
                    IdSpecialization = 1,
                    Price = 150,
                    Description = "hi-hi ha-ha",
                    IdBrigade = 1,
                },
                new RepairCompanyManagement.DataAccess.Entities.Task()
                {
                    Id = items[1].Id,
                    Title = "Electikst1",
                    IdSpecialization = 1,
                    Price = 150,
                    Description = "hi-hi ha-ha",
                    IdBrigade = 1,
                },
            });

        }

        [Test]
        public void CreateNewTask_WhenIdSpecializationIsNegativ_ShouldReturnSqlException()
        {
            Action item = () => _taskRepository.Create(
                new RepairCompanyManagement.DataAccess.Entities.Task
                {
                    Id = 1,
                    Title = "title",
                    IdSpecialization = -1,
                    Price = 150,
                    Description = "desctiprion",
                    IdBrigade = 1,
                });

            item.Should().Throw<System.Data.SqlClient.SqlException>();
        }

        /*    [Test]
            public void CreateNewTask_WhenPriceIsNegativ_ShouldReturnSqlException()
            {

                Action item = () => _taskRepository.Create(
                    new RepairCompanyManagement.DataAccess.Entities.Task
                    {
                        Id = 1,
                        Title = "title",
                        IdSpecialization = 1,
                        Price = -150,
                        Description = "desctiprion",
                        IdBrigade = 1,
                    });
                *//*var items = _taskRepository.GetAll().ToList();
                _taskRepository.Delete(items[1].Id);*//*

                item.Should().Throw<System.Data.SqlClient.SqlException>();
            } */

        [Test]
        public void CreateNewTask_WhenIdBrigadeIsNegativ_ShouldReturnSqlException()
        {
            Action item = () => _taskRepository.Create(
                new RepairCompanyManagement.DataAccess.Entities.Task
                {
                    Id = 1,
                    Title = "title",
                    IdSpecialization = 1,
                    Price = 150,
                    Description = "desctiprion",
                    IdBrigade = -1,
                });

            item.Should().Throw<System.Data.SqlClient.SqlException>();
        }

        [Test]
        public void CreateNewTask_WhenTitleIsNull_ShouldReturnSqlException()
        {
            Action item = () => _taskRepository.Create(
                new RepairCompanyManagement.DataAccess.Entities.Task
                {
                    Id = 1,
                    Title = null,
                    IdSpecialization = 1,
                    Price = 150,
                    Description = "desctiprion",
                    IdBrigade = 1,
                });

            item.Should().Throw<System.Data.SqlClient.SqlException>();
        }

        [Test]
        public void CreateNewTask_WhenDescriptionIsNull_ShouldReturnSqlException()
        {
            Action item = () => _taskRepository.Create(
                new RepairCompanyManagement.DataAccess.Entities.Task
                {
                    Id = 1,
                    Title = "title",
                    IdSpecialization = 1,
                    Price = 150,
                    Description = null,
                    IdBrigade = 1,
                });

            item.Should().Throw<System.Data.SqlClient.SqlException>();
        }

        [Test]
        public void DeleteTask_WhenIdIsCorrect_ShouldDeleteTask()
        {
            var item = _taskRepository.Create(
                new RepairCompanyManagement.DataAccess.Entities.Task
                {
                    Id = 1,
                    Title = "title",
                    IdSpecialization = 1,
                    Price = 150,
                    Description = "Description",
                    IdBrigade = 1,
                });
            var items = _taskRepository.GetAll().ToList();
            _taskRepository.Delete(items[1].Id);
            items = _taskRepository.GetAll().ToList();

            items.Should().BeEquivalentTo(new List<RepairCompanyManagement.DataAccess.Entities.Task>
            {
                new RepairCompanyManagement.DataAccess.Entities.Task()
                {
                    Id = 1,
                    Title = "Electikst",
                    IdSpecialization = 1,
                    Price = 150,
                    Description = "hi-hi ha-ha",
                    IdBrigade = 1,
                }
            });

        }
    }
}
