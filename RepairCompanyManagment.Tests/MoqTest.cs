using Moq;
using NUnit.Framework;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.DataAccess.Interfaces;
using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace RepairCompanyManagment.Tests
{
    [TestFixture]
    public class MoqTest
    {
        List<Brigade> brigadesDAL = new List<Brigade>()
        {
            new Brigade(){Id = 1, Title = "Chom", IdSpecialization = 1},
            new Brigade(){Id = 2, Title = "Chom", IdSpecialization = 1},
            new Brigade(){Id = 3, Title = "Nikel", IdSpecialization = 2},
        };

        List<BrigadeDto> brigadesBLL = new List<BrigadeDto>()
        {
            new BrigadeDto(){Id = 1, Title = "Chom", IdSpecialization = 1},
            new BrigadeDto(){Id = 2, Title = "Chom", IdSpecialization = 1},
            new BrigadeDto(){Id = 3, Title = "Nikel", IdSpecialization = 2},
        };

        List<OrderTaskDto> orderTaskDtos = new List<OrderTaskDto>()
        {
            new OrderTaskDto(){ Id = 1, IdOrder = 1, IdTask = 1, Description = "", Status = 1, TaskCompletionDate = new DateTimeOffset() },
            new OrderTaskDto(){ Id = 2, IdOrder = 1, IdTask = 2, Description = "", Status = 1, TaskCompletionDate = new DateTimeOffset() },
            new OrderTaskDto(){ Id = 3, IdOrder = 2, IdTask = 3, Description = "", Status = 1, TaskCompletionDate = new DateTimeOffset() },
            new OrderTaskDto(){ Id = 4, IdOrder = 3, IdTask = 4, Description = "", Status = 1, TaskCompletionDate = new DateTimeOffset() },
        };

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void DAL_GetBrigade_MoqObjectShoudBeReturnEquals()
        {
            var mock = new Mock<IRepository<Brigade>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetBrigades());
            var items = GetBrigades();

            var result = mock.Object;
            items.Should().BeEquivalentTo(result.GetAll().ToList());
        }

        [Test]
        public void DAL_GetBrigadeById_MoqObjectShoudBeReturnEquals()
        {
            var mock = new Mock<IRepository<Brigade>>();
            mock.Setup(repo => repo.GetById(1)).Returns(GetBrigadeById(1));
            var item = GetBrigadeById(1);

            var result = mock.Object;

            item.Should().BeEquivalentTo(result.GetById(1));
        }

        [Test]
        public void DAL_CreateBrigade_MoqObjectShoudBeReturnEquals()
        {
            Brigade brigade = new Brigade() { Id = 4, Title = "Cupryme", IdSpecialization = 1 };
            var mock = new Mock<IRepository<Brigade>>();
            mock.Setup(repo => repo.Create(brigade)).Returns(CreateBrigade());

            var result = mock.Object;
            result.Create(brigade);

            brigadesDAL.Should().BeEquivalentTo(GetBrigades());

            DeleteBrigade(4);
        }

        [Test]
        public void DAL_DeleteBrigade_MoqObjectShoudBeReturnEquals()
        {
            var mock = new Mock<IRepository<Brigade>>();
            mock.Setup(repo => repo.Delete(3));

            var result = mock.Object;
            result.Delete(3);

            brigadesDAL.Should().BeEquivalentTo(GetBrigades());

            brigadesDAL.Add(new Brigade() { Id = 3, Title = "Nikel", IdSpecialization = 2 });
        }

        [Test]
        public void BLL_GetBrigade_MoqObjectShoudBeReturnEquals()
        {
            var mock = new Mock<IBrigadeService>();
            mock.Setup(serv => serv.GetAllBrigades()).Returns(GetBrigadesBLL());
            var items = GetBrigadesBLL();

            var result = mock.Object;

            items.Should().BeEquivalentTo(result.GetAllBrigades().ToList());

        }

        [Test]
        public void BLL_GetBrigadeById_MoqObjectShoudBeReturnEquals()
        {
            var mock = new Mock<IBrigadeService>();
            mock.Setup(serv => serv.GetBrigadeById(1)).Returns(GetBrigadeByIdBLL(1));

            var result = mock.Object;

            GetBrigadeByIdBLL(1).Should().BeEquivalentTo(result.GetBrigadeById(1));
        }

        [Test]
        public void BLL_FindOrderTaskByOrderAndTaskIds_MoqObjectShouldBeReturnIndexOfOrderTask()
        {
            var mock = new Mock<IOrderService>();
            mock.Setup(serv => serv.FindOrderTaskByOrderAndTaskIds(1, 2)).Returns(2);

            var result = mock.Object;

            var item = result.FindOrderTaskByOrderAndTaskIds(1, 2);
            Assert.AreEqual(item, 2);
        }

        public Brigade GetBrigadeById(int id)
        {
            return brigadesDAL[id];
        }

        public BrigadeDto GetBrigadeByIdBLL(int id)
        {
            return brigadesBLL[id];
        }
        public int CreateBrigade()
        {
            brigadesDAL.Add(new Brigade() { Id = 4, Title = "Cupryme", IdSpecialization = 1 });

            return 4;
        }

        public List<Brigade> GetBrigades()
        {
            return brigadesDAL;
        }
        public List<BrigadeDto> GetBrigadesBLL()
        {
            return brigadesBLL;
        }
        public int DeleteBrigade(int id)
        {
            var item = brigadesDAL.FirstOrDefault(x => x.Id == id);
            brigadesDAL.Remove(item);

            return 1;
        }

        
    }
}
