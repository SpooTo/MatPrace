using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TaskManager;
using TaskManager.Entities;
using TaskWebProject.Models;
using TaskManager.Managers;
using Microsoft.AspNet.Identity;




namespace TaskWebProject.Controllers
{

    public class TaskController : Controller
    {
        TaskManager.Managers.TaskManager tm = new TaskManager.Managers.TaskManager();
        ProjectManager pm = new TaskManager.Managers.ProjectManager();
        TaskEntities te = new TaskEntities();
        ApplicationDbContext adc = new ApplicationDbContext();

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(FormCollection fc, int TaskId)
        {
            Task t = tm.Read(TaskId);
            var st = t.state;
            return View(t);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Task t = tm.Read(id);
            ViewBag.Users = adc.Users.ToList();
            return View(t);
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            int id = tm.Create(task);

            return RedirectToAction("Details", id);
        }
        [HttpPost]
        public ActionResult CreateFromProjectDetails(FormCollection fc, int id)
        {
            Task task = new Task();
            task.title = fc["tit"];
            task.text = fc["text"];
            task.deadline = DateTime.Today;
            task.priority = (Priorities)Enum.Parse(typeof(Priorities), fc["priority"]);
            //task.deadline = Convert.ToDateTime(fc["deadline"]);
            Project p = pm.Read(id);
            p.tasks.Add(task);
            tm.Create(task);


            return RedirectToAction("Details", "Project", new { id = id });
        }

        [HttpPost]
        public ActionResult Update(int id, Task t)
        {
            tm.Update(id, t);

            return RedirectToAction("Details", id);
        }


        public ActionResult Delete(int id)
        {
            tm.Delete(id);
            int task = id;
            Project project = tm.GetTaskProject(id);
            return RedirectToAction("Details", "Project", new { id = project.id });
        }


        public ActionResult Assign(FormCollection fc, int TaskID)
        {
            Task task = tm.Read(TaskID);
            string UserID = fc["user"];
            tm.Assign(task, UserID);
            return RedirectToAction("Details", new { id = TaskID });

        }


        public ActionResult Take(int TaskID)
        {
            Task task = tm.Read(TaskID);
            tm.Assign(task, User.Identity.GetUserId());
            return RedirectToAction("Details", new { id = TaskID });
        }

    }
}