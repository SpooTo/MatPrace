using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Entities;
using System.Security.Principal;
using System.Data.Entity;


namespace TaskManager.Managers
{
    public class TeamManager : Manager<Team>
    {

        public override int Create(Team entity)
        {
            db.teams.Add(entity);
            db.SaveChanges();
            int id = entity.id;
            return id;
        }

        public override Team Read(int id)
        {
            return db.teams.SingleOrDefault(t => t.id == id);
        }

        public override void Update(int id, Team entity)
        {
            db.Entry(Read(id)).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public override void Delete(int id)
        {
            ProjectManager pm = new ProjectManager();
            Team t = Read(id);
            foreach (TeamsMembers teamMember in db.teamsMembers.Where(x => x.team.id == id))
            {
                db.teamsMembers.Remove(teamMember);
            }
            
            foreach (Project p in t.projects.ToList())
            {
                pm.Delete(p.id);
            }
            db.teams.Remove(t);
            db.SaveChanges();
        }

        /// <summary>
        /// Obtains team mebers ids
        /// </summary>
        /// <param name="team">Team</param>
        /// <returns>List of user ids</returns>
        public static List<string> GetTeamMembersIds(Team team)
        {
            List<string> members = new List<string>();
            foreach (TeamsMembers teamMember in db.teamsMembers.Where(x => x.team.id == team.id))
            {
                members.Add(teamMember.userId);
            }
            return members;
        }

        public bool IsMember (Team t, string id)
        {
            TeamsMembers tm = db.teamsMembers.SingleOrDefault(x => (x.userId == id) && (x.team.id == t.id));
            bool answer = false;
            if(tm!=null)
            {
                answer = true;
            }
            return answer;
        }

        /// <summary>
        /// Obtains teams in which is user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Team> GetUserTeams(string id)
        {
            return db.teamsMembers.Where(x => x.userId == id).Select(x => x.team).ToList();
        }

        /// <summary>
        /// Metoda přiřadí Usera k Teamu
        /// </summary>
        /// <param name="UserId">Id uživatele</param>
        /// <param name="t">Team ke kterému se uživatel přidává</param>
        public void AddMemberToTeam(string UserId, Team t)
        {

            TeamsMembers tm = new TeamsMembers();
            tm.team = t;
            tm.userId = UserId;
            db.teamsMembers.Add(tm);
            db.SaveChanges();

        }

        /// <summary>
        /// Metoda odstrani Usera z Teamu
        /// </summary>
        /// <param name="UserId">Id uživatele</param>
        /// <param name="t">Team ze ktereho uzivatele odebrat</param>
        public void RemoveMemberFromTeam(string UserId, Team t)
        {
            TeamsMembers tm = db.teamsMembers.SingleOrDefault(x => (x.userId == UserId) && (x.team.id == t.id));           
            db.teamsMembers.Remove(tm);
            db.SaveChanges();
        }
    }
}
