using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.DataAccess.Entities;
using System.Collections.Generic;

namespace RepairCompanyManagement.BusinessLogic.Interfaces
{
    public interface IBrigadeService
    {
        int CreateSpecialization(SpecializationDto item);

        SpecializationDto GetSpecializationById(int id);

        IReadOnlyCollection<SpecializationDto> GetAllSpecializations();

        void UpdateSpecialization(SpecializationDto item);

        void DeleteSpecialization(int id);

        void ValidateSpecialization(SpecializationDto item);

        /////////////////////////////////

        // /// / / / / /// / / / / /  //  //  // 

        int CreateOrderTask(OrderTaskDto item);

        OrderTaskDto GetOrderTaskById(int id);

        IReadOnlyCollection<OrderTaskDto> GetAllOrderTasks();

        void UpdateOrderTask(OrderTaskDto item);

        void DeleteOrderTask(int id);

        void ValidateOrderTask(OrderTaskDto item);
        /////////////////////////////////
        int CreateTask(TaskDto item);

        TaskDto GetTaskById(int id);

        IReadOnlyCollection<TaskDto> GetAllTasks();

        void UpdateTask(TaskDto item);

        void DeleteTask(int id);

        void ValidateTask(TaskDto item);

        // /// / / / / /// / / / / /  //  //  // 
        int CreateBrigade(BrigadeDto item);

        BrigadeDto GetBrigadeById(int id);

        IReadOnlyCollection<BrigadeDto> GetAllBrigades();

        void UpdateBrigade(BrigadeDto item);

        void DeleteBrigade(int id);

        IReadOnlyCollection<BrigadeDto> FindBrigadeBySpecialization(int specializationId);
    }
}
