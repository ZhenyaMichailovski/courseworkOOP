using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Enums
{
    public enum OrderStatus : int
    {
        NotConfirmed = 1,
        Paid,
        Confirmed,
        Completed,
        Сanceled
    }
}