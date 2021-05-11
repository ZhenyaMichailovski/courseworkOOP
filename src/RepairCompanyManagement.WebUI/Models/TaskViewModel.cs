using System;
using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;
using RepairCompanyManagement.WebUI.Enums;
using System.Collections.Generic;

namespace RepairCompanyManagement.WebUI.Models
{
    public class TaskViewModel : TaskSpecializationViewModel
    {
        [Display(Name = "Task")]
        public int Id { get; set; }

        //[Required(ErrorMessage = Constants.EmptyTaskTitleMessage)]
        public string Title { get; set; }

        public string SpecializationName { get; set; }
        public string Owner { get; set; }
        public IEnumerable<TaskItem> TaskItems { get; set; }
        public int IdBrigade { get; set; }
        public string BrigadeName { get; set; }

        [Required(ErrorMessage = Constants.EmptyTaskTitleMessage)]
        public double Price { get; set; }

      //  [Required(ErrorMessage = Constants.EmptyTaskTitleMessage)]
        public string Description { get; set; }
        public DateTimeOffset TaskCompletionDate { get; set; }
        public OrderTaskStatus Status { get; set; }
    }
}