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
    public class DesignController : Controller
    {
        
        TaskEntities te = new TaskEntities();
        ApplicationDbContext adc = new ApplicationDbContext();

        // GET: Design
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TeamList()
        {
            return View(te.teams);
        }


        [HttpPost]
        public ActionResult TeamDetails(FormCollection fc, int TeamId)
        {
            Team t = te.teams.SingleOrDefault(x => x.id == TeamId);
            return View(t);
        }
        [HttpGet]
        public ActionResult TeamDetails(int id)
        {
            Team t = te.teams.SingleOrDefault(x => x.id == id);
            ViewBag.AppUsers = new ApplicationDbContext().Users;
            return View(t);
        }


        [HttpPost]
        public ActionResult ProjectDetails(FormCollection fc, int ProjectId)
        {
            Project p = te.projects.SingleOrDefault(x => x.id == ProjectId);
            int TeamId = Convert.ToInt32(fc["team"]);
            
            Team t = te.teams.SingleOrDefault(x => x.id == TeamId);

            ViewBag.teams = te.teams;
            return View(p);
        }
        [HttpGet]
        public ActionResult ProjectDetails(int id)
        {
            Project p = te.projects.SingleOrDefault(x => x.id == id);
            ViewBag.teams = te.teams;
            return View(p);
        }


        [HttpPost]
        public ActionResult TaskDetails(FormCollection fc, int TaskId)
        {
            Task t = te.tasks.SingleOrDefault(x => x.id == TaskId);
            var st = t.state;
            return View(t);
        }
        [HttpGet]
        public ActionResult TaskDetails(int id)
        {
            Task t = te.tasks.SingleOrDefault(x => x.id == id);
            return View(t);
        }



        public ActionResult ProjectList()
        {
            ViewBag.teams = te.teams;
            return View(te.projects);
        }


        public ActionResult UserList()
        {
            return View(adc.Users);
        }


        [HttpPost]
        public ActionResult UserDetails()
        { 
            return View();
        }
        [HttpGet]
        public ActionResult UserDetails(string id)
        {
            ViewBag.members = TeamManager.GetUserTeams(id);
            ApplicationUser u = adc.Users.SingleOrDefault(x => x.Id == id);
            return View(u);
        }



        //[HttpPost]
        //public ActionResult ProjectEdit(FormCollection fc, int ProjectId)
        //{
        //    int TeamId = Convert.ToInt32(fc["ddlSetTeam"]);
        //    Project p = te.projects.SingleOrDefault(x => x.id == ProjectId);
        //    Team t = te.teams.SingleOrDefault(x => x.id == TeamId);


        //    return RedirectToAction("ProjectList");
        //}

    }
}