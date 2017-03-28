using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Entities
{
    public class Project
    {
        public int id { get; set; }
        [Required, MaxLength(50)]
        public string name { get; set; }
        [Required, MaxLength(50)]
        public string description { get; set; }
        public virtual List<Task> tasks { get; set; }
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }

        public Project()
        {
            tasks = new List<Task>();
        }

    }


}
