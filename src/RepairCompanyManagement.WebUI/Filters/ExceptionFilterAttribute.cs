using RepairCompanyManagement.BusinessLogic.Exceptions;
using RepairCompanyManagement.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepairCompanyManagement.WebUI.Filters
{
    public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        private const string DefaultRedirectionActionName = "Index";

        public string RedirectionControllerName { get; set; }

        public string RedirectionActionName { get; set; }

        public object RedirectionRouteValues { get; set; }

        public string Message { get; set; }

        public ExceptionFilterAttribute(string redirectionAction = null, string redirectionController = null, object routeValues = null)
        {
            RedirectionControllerName = redirectionController;
            RedirectionActionName = redirectionAction;
            RedirectionRouteValues = routeValues;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                if(filterContext.Exception is BusinessLogicException ||
                    filterContext.Exception is ValidationException)
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<ErrorViewModel>(new ErrorViewModel
                    {
                        ErrorMessage = filterContext.Exception.Message,
                        RedirectionControllerName = RedirectionControllerName ?? filterContext.RouteData.Values["controller"].ToString(),
                        RedirectionActionName = RedirectionActionName ?? DefaultRedirectionActionName,
                        RedirectionRouteValues = RedirectionRouteValues,
                    }),
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}