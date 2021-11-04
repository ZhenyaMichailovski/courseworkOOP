using NUnit.Framework;
using RepairCompanyManagement.DataAccess.Repositories;
using RepairCompanyManagement.DataAccess.Exceptions;
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
        public void GetTaskById_WhenAnItemIsNotInTheDatabase_ShouldReturnNull(int index)
        {
            var item = _taskRepository.GetById(index);

            item.Should().BeNull();
        }


        [Test]
        public void CreateNewTask_WhenTheAllItemsIsCorrect_ShouldReturnNewItem()
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
            var items = _taskRepository.GetById(item);
            _taskRepository.Delete(item);
            items.Should().BeEquivalentTo(new RepairCompanyManagement.DataAccess.Entities.Task
            {
                Id = item,
                Title = "Electikst1",
                IdSpecialization = 1,
                Price = 150,
                Description = "hi-hi ha-ha",
                IdBrigade = 1,
            });

        }

        [Test]
        [TestCase(-1, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", -1, 9999999999999999999.99,
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", -1)]
        public void CreateNewTask_WhenAllDateIsNotCorrect_ShouldReturnDateException(
            int id, string title, int idSpecialization, decimal price, string description, int idBrigade)
        {
            Action item = () => _taskRepository.Create(
                new RepairCompanyManagement.DataAccess.Entities.Task
                {
                    Id = id,
                    Title = title,
                    IdSpecialization = idSpecialization,
                    Price = price,
                    Description = description,
                    IdBrigade = idBrigade,
                });

            item.Should().Throw<DataException>();
        }

        [Test]
        [TestCase(1, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 1, 999999999999999999.99,
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 1)]
        public void CreateNewTask_WhenAllDateIsBoundary_ShouldAddNewItem(
            int id, string title, int idSpecialization, decimal price, string description, int idBrigade)
        {
            var item = _taskRepository.Create(
               new RepairCompanyManagement.DataAccess.Entities.Task
               {
                   Id = id,
                   Title = title,
                   IdSpecialization = idSpecialization,
                   Price = price,
                   Description = description,
                   IdBrigade = idBrigade,
               });

            var items = _taskRepository.GetById(item);
            _taskRepository.Delete(item);
            items.Should().BeEquivalentTo(new RepairCompanyManagement.DataAccess.Entities.Task
            {
                Id = item,
                Title = title,
                IdSpecialization = idSpecialization,
                Price = price,
                Description = description,
                IdBrigade = idBrigade,
            });
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
