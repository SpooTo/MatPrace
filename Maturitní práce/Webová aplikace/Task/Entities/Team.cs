using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Entities
{
    public class Team
    {
        public int id { get; set; }
        [Required, MaxLength(50)]
        public string title { get; set; }
        [Required, MaxLength(100)]
        public string description { get; set; }
        public virtual List<Project> projects { get; set; }
        public string LeaderId { get; set; }


    }
}
