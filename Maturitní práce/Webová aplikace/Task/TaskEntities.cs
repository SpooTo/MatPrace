namespace TaskManager
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TaskManager.Entities;

    public class TaskEntities : DbContext
    {     
        public TaskEntities()
            : base("TaskEntities")
        {
        }
        
        public virtual DbSet<Task> tasks { get; set; }
        public virtual DbSet<Project> projects { get; set; }
        public virtual DbSet<Team> teams { get; set; }
        public virtual DbSet<TeamsMembers> teamsMembers { get; set; }
    }

    
}