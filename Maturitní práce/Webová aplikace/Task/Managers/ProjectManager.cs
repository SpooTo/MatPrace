using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Entities;

namespace TaskManager.Managers
{
    public class ProjectManager : Manager<Project>
    {

        public override int Create(Project entity)
        {

            int id = db.projects.Add(entity).id;

            db.SaveChanges();
            return id;
        }

        public IQueryable<Project> List()
        {
            return db.projects;
        }

        public override Project Read(int id)
        {
            return db.projects.SingleOrDefault(t => t.id == id);
        }

        public override void Update(int id, Project entity)
        {
            db.Entry(Read(id)).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public override void Delete(int id)
        {
            TaskManager tm = new TaskManager();
            Project p = Read(id);
            List<Task> tasks = GetProjectTasks(id);
            foreach (Task t in tasks)
            {
                tm.Delete(t.id);
            }
            db.SaveChanges();
            db.projects.Remove(p);
            db.SaveChanges();
        }

        public static List<Task> GetProjectTasks(int ProjectID)
        {
            ProjectManager pm = new ProjectManager();
            return pm.Read(ProjectID).tasks.ToList();
        }

        public static Team GetMainTeam(Project project)
        {
            return db.teams.FirstOrDefault(x => x.projects.Select(y => y.id).Contains(project.id));
        }

        public static List<Project> GetUserProjects(string userID)
        {
            List<Project> projects = new List<Project>();
            foreach (Team t in TeamManager.GetUserTeams(userID))
            {
                projects.AddRange(t.projects);
            }
            return projects;
        }

        public static void AddProjectToTeam(Project entity, Team t)
        {
            t.projects.Add(entity);
            db.SaveChanges();
        }




    }
}
