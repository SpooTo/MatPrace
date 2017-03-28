using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.UI;

namespace TaskManager.Entities
{
    public class Task
    {
        public int id { get; set; }
        [Required, MaxLength(200)]
        public string text { get; set; }
        [Required, MaxLength(50)]
        public string title { get; set; }
        [Required]
        public Priorities priority { get; set; }

        public DateTime datecreated { get; set; }
        public DateTime deadline { get; set; }
        public TaskStates state { get; set; }
        public string assigned { get; set; }
    }

    public enum TaskStates
    {
        [VisualMeaning("warning")]
        New,
        [VisualMeaning("primary")]
        Assigned,
        [VisualMeaning("success")]
        Done,
        [VisualMeaning("danger")]
        Overdue,
        [VisualMeaning("default")]
        Closed
    }

    public enum Priorities
    {
        Critical,
        VeryHigh,
        High,
        Medium,
        Low,
        VeryLow,
        None
    }
}
