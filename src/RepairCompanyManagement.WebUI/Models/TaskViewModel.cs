using System;
using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;
using RepairCompanyManagement.WebUI.Enums;

namespace RepairCompanyManagement.WebUI.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.EmptyTaskTitleMessage)]
        public string Title { get; set; }

        public string Owner { get; set; }

        [Required(ErrorMessage = Constants.EmptyTaskTitleMessage)]
        public int IdSpecialization { get; set; }
        public string SpecializationName { get; set; }

        public int IdBrigade { get; set; }
        public string BrigadeName { get; set; }

        [Required(ErrorMessage = Constants.EmptyTaskTitleMessage)]
        public double Price { get; set; }

        [Required(ErrorMessage = Constants.EmptyTaskTitleMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = Constants.EmptyTaskTitleMessage)]
        public DateTimeOffset TaskCompletionDate { get; set; }
        public TaskStatus Status { get; set; }
    }
}