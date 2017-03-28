using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Entities
{
    public class TeamsMembers
    {
        public int id { get; set; }
        public string userId { get; set; }
        public Team team { get; set; }
    }
}

