using RepairCompanyManagement.WebUI.Filters;
using System.Web;
using System.Web.Mvc;

namespace RepairCompanyManagement.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
