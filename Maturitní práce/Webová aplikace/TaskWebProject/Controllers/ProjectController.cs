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


namespace TaskWebProject.Controllers
{

    public class ProjectController : Controller
    {
        TeamManager tm = new TeamManager();
        ProjectManager pm = new ProjectManager();
        TaskEntities te = new TaskEntities();
        ApplicationDbContext adc = new ApplicationDbContext();

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(FormCollection fc, int ProjectId)
        {
            Project p = pm.Read(ProjectId);
            int TeamId = Convert.ToInt32(fc["team"]);

            Team t = tm.Read(TeamId);

            ViewBag.teams = te.teams;
            return View(p);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Project p = pm.Read(id);
            ViewBag.teams = te.teams;
            return View(p);
        }



        [HttpPost]
        public ActionResult List(Project project)
        {
            ViewBag.teams = te.teams;
            return View(project);
        }

        [HttpGet]
        public ActionResult List()
        {
            ViewBag.projects = te.projects;
            ViewBag.teams = te.teams;
            return View();
        }


        [HttpPost]
        public ActionResult Create(Project project)
        {
            pm.Create(project);

            return RedirectToAction("List");
        }


        [HttpPost]
        public ActionResult CreateFromTeamDetails(FormCollection fc, int id)
        {
            Project project = new Project();
            project.name = fc["name"];
            project.description = fc["desc"];
            Team t = tm.Read(id);
            project.Team = t;

            pm.Create(project);

            return RedirectToAction("Details", "Team", new { id = id });
        }

        [HttpPost]
        public ActionResult Update(int id, Project p)
        {
            pm.Update(id, p);
            return RedirectToAction("Details", new { id = id });
        }


        public ActionResult Delete(int id)
        {
            pm.Delete(id);

            return RedirectToAction("List");
        }
    }
}