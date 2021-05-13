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
        public ActionResult GetReportByOrdersInMonth(int month)
        {
            var items = _reportService.GetReportForMonth(month);

            Chart chart = new Chart(width: 700, height: 300)
                .AddTitle($"Statistics for  for brigades")
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