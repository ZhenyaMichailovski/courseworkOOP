using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.WebUI.Filters;
using RepairCompanyManagement.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RepairCompanyManagement.WebUI.Controllers
{
    [Authorize(Roles =Identity.IdentityConstants.AdminRole)]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportsController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SelectMonth()
        {
            var model = new ReportMonthViewModel
            {
                MonthId = 0,
                MonthName = "",
                Months = _mapper.Map<List<Month>>(_reportService.GetAllMonth()),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectMonth(ReportMonthViewModel model)
        {
            if(model != null && ModelState.IsValid)
            {
                return RedirectToAction("GetReportByOrdersInMonth", "Reports", new { month = model.MonthId, monthName = model.MonthName });
            }
            return View("SelectMonth", "Reports");
        }

        [HttpGet]
        public ActionResult GetReportByOrdersInYear()
        {
            var items = _reportService.GetReportForYear();

            Chart chart = new Chart(width: 700, height: 300)
                .AddTitle($"Statistics for this year for brigades")
                .AddSeries(
                    chartType: "StackedBar",
                    xValue: items.Select(it => it.Brigade).ToArray(),
                    yValues: items.Select(it => it.OrderAmount).ToArray()
                )
                .Write();

            return null;
        }
        [HttpGet]
        public ActionResult GetReportByOrdersInMonth(int month, string monthName )
        {
            var items = _reportService.GetReportForMonth(month);

            Chart chart = new Chart(width: 700, height: 300)
                .AddTitle($"Statistics for "+ monthName +" for brigades")
                .AddSeries(
                    chartType: "StackedBar",
                    xValue: items.Select(it => it.Brigade).ToArray(),
                    yValues: items.Select(it => it.OrderAmount).ToArray()
                )
                .Write();

            return null;
        }
    }
}