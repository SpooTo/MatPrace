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


    public class TeamController : Controller
    {
        TeamManager tm = new TeamManager();
        TaskEntities te = new TaskEntities();
        ApplicationDbContext adc = new ApplicationDbContext();

        // GET: Team
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(FormCollection fc, int id)
        {

            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            Team team = new Team();
            ViewBag.teams = te.teams;
            return View(team);
        }


        public ActionResult Details(int id)
        {
            Team t = tm.Read(id);
            ViewBag.isMember = tm.IsMember(t, User.Identity.GetUserId());
            ViewBag.AppUsers = new ApplicationDbContext().Users;
            return View(t);
        }

        [HttpPost]
        public ActionResult Create(Team team)
        {

            int id = tm.Create(team);

            return RedirectToAction("Details",new { id = id });
        }


        [HttpPost]
        public ActionResult Update(int id, Team t)
        {
            tm.Update(id, t);

            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult Delete(int id)
        {
            tm.Delete(id);

            return RedirectToAction("List");
        }


        public ActionResult Join(int id)
        {
            Team t = tm.Read(id);
            string UserId = User.Identity.GetUserId();
            tm.AddMemberToTeam(UserId, t);

            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult Leave(int id)
        {
            Team t = tm.Read(id);
            string UserId = User.Identity.GetUserId();
            tm.RemoveMemberFromTeam(UserId, t);

            return RedirectToAction("Details", new { id = id });
        }

    }
}