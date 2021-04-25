using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.DataAccess.Entities;
using System.Collections.Generic;

namespace RepairCompanyManagement.BusinessLogic.Interfaces
{
    public interface IWorkerService
    {
        
        /////////////////////////////////
        

        /////////////////////////////////
        int CreateEmployee(EmployeeDto item);

        EmployeeDto GetEmployeeById(int id);

        IReadOnlyCollection<EmployeeDto> GetAllEmployees();

        void UpdateEmployee(EmployeeDto item);

        void DeleteEmployee(int id);



        int CreateBrigade(BrigadeDto item);

        BrigadeDto GetBrigadeById(int id);

        IReadOnlyCollection<BrigadeDto> GetAllBrigades();

        void UpdateBrigade(BrigadeDto item);

        void DeleteBrigade(int id);

        // /// / / / / /// / / / / /  //  //  // 

        // /// / / / / /// / / / / /  //  //  // 

        int CreateJobPosition(JobPositionDto item);

        JobPositionDto GetJobPositionById(int id);

        IReadOnlyCollection<JobPositionDto> GetAllJobPositions();

        void UpdateJobPosition(JobPositionDto item);

        void DeleteJobPosition(int id);

        void ValidateJobPosition(JobPositionDto item);
        /////////////////////////////////

    }
}
