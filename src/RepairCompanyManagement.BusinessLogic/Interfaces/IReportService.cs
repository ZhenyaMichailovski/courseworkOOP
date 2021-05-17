using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairCompanyManagement.BusinessLogic.Dtos;

namespace RepairCompanyManagement.BusinessLogic.Interfaces
{
    public interface IReportService
    {
        IList<ReportYearDto> GetReportForYear();
        IList<ReportYearDto> GetReportForMonth(int month);
        IList<MonthDto> GetAllMonth();
    }
}
