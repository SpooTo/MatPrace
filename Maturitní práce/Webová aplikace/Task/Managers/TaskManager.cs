using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Entities;
using System.Globalization;

namespace TaskManager.Managers
{
    public class TaskManager : Manager<Task>
    {


        public override int Create(Task entity)
        {
            entity.datecreated = DateTime.UtcNow;
            int id = db.tasks.Add(entity).id;
            db.SaveChanges();
            return id;
        }

        public override Task Read(int id)
        {
            return db.tasks.SingleOrDefault(t => t.id == id);
        }

        public override void Update(int id, Task entity)
        {
            db.Entry(Read(id)).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public override void Delete(int id)
        {
            db.tasks.Remove(Read(id));
        }

        public static List<Task> GetUserTasks(string userID)
        {
            List<Task> tasks = new List<Task>();
            List<Project> projects = ProjectManager.GetUserProjects(userID);
            foreach (Project p in projects)
            {
                tasks.AddRange(p.tasks.Where(x => x.assigned == null).ToList());
            }
            return tasks;
        }

        public Project GetTaskProject(int id)
        {
            ProjectManager pm = new ProjectManager();
            return pm.List().FirstOrDefault(x => x.tasks.Select(y => y.id).Contains(id));


        }

        public void Assign(Task task, string userId)
        {

            if (task.state != TaskStates.Overdue)
            {
                task.state = TaskStates.Assigned;
            }
            task.assigned = userId;
            db.SaveChanges();
        }

        #region FILTERS
        public List<Task> getByTitle(string nameSearched)
        {
            return db.tasks.Where(t => t.title.Contains(nameSearched)).ToList();
        }

        public List<Task> getByTitle(string nameSearched, bool exactlySame)
        {
            return (exactlySame) ? db.tasks.Where(t => t.title.Equals(nameSearched)).ToList() : getByTitle(nameSearched);
        }

        public List<Task> getByText(string descSearched)
        {
            return db.tasks.Where(t => t.text.Contains(descSearched)).ToList();
        }

        public List<Task> getByState(string stateSearched)
        {
            TaskStates s;
            if (Enum.TryParse(stateSearched, out s))
            {
                return db.tasks.Where(t => t.state.Equals(s)).ToList();
            }
            return null;
        }

        public List<Task> getByPriority(string prioritySearched)
        {
            TextInfo ti = new CultureInfo("en-US", false).TextInfo;
            prioritySearched = ti.ToTitleCase(prioritySearched).Replace(" ", "");
            Priorities p;
            if (Enum.TryParse(prioritySearched, out p))
            {
                return db.tasks.Where(t => t.priority.Equals(p)).ToList();
            }
            return null;
        }

        public List<Task> getByAsignee(string asigneeSearched)
        {
            return db.tasks.Where(t => t.assigned.Equals(asigneeSearched)).ToList();
        }

        //ddmmyyyy
        public List<Task> getByDeadline(string dateSearched)
        {
            DateTime dt = DateTime.ParseExact(dateSearched, "yyyyMMdd", null);

            throw new NotImplementedException();
        }

        public List<Task> getByDateCreated(string dateSearched)
        {
            throw new NotImplementedException();
        }

        public List<Task> getByDates(string DateSearched)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
