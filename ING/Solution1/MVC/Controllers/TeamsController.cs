using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data2;
using Data2.DTOs;

namespace MVC.Controllers
{
   
    public class TeamsController : Controller
    {
        // GET: Teams  
        // ovde treba neka druga premisija, ali tehnicki ova radi
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewLeavesList)]
        public ActionResult Index()
        {
            try
            {
                Models.TeamsPageModel model = Models.TeamsPageModel.Load();
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult Details(int id)
        {
           return RedirectToAction("Index", "Employees", new { teamFilter = id });
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult Delete(int id)
        {
            try
            {

                Data2.Teams.removeTeam(id);
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult History(int id)
        {
            try
            {

                Models.TeamHistoryModel model = Models.TeamHistoryModel.Load(id);
                return View(model);

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult TeamsOfEmployee(int id, Boolean current = true)
        {
            try
            {

                Models.TeamsOfEmployeeModel model = Models.TeamsOfEmployeeModel.Load(id, current);
                return View(model);

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }


        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        [HttpPost]
        public ActionResult Edit(Data2.DTOs.TeamDTO model)
        {
            try
            {

                Teams.changeTeamName(model.TeamID, model.TeamName);
                Data2.DTOs.TeamDTO model2 = Models.TeamsPageModel.createTeamDTO(Data2.Teams.getTeamAt(model.TeamID));


                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

       
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult Edit(int id)
        {
            try
            {

                Data2.DTOs.TeamDTO model = Models.TeamsPageModel.createTeamDTO(Data2.Teams.getTeamAt(id));
                return View(model);

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult RemoveEmployee(int teamID, int empID)
        {
            try
            {
                Teams.removeEmployeeFromTeam(empID, teamID);
                return RedirectToAction("History", new { id = teamID });

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult Create()
        {
            return View();
        }

        //[AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        //[HttpPost]
        //public ActionResult Create(Data2.DTOs.TeamDTO team)
        //{
        //    Teams.addTeam(team.TeamName);
        //    return RedirectToAction("Index");
        //}

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        [HttpPost]
        public ActionResult Create(Data2.DTOs.TeamDTO model)
        {
            try
            {

                Teams.addTeam(model.TeamName);
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        //[AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        //[HttpPost]
        //public ActionResult Create(String name)
        //{
        //    Teams.addTeam(name);
        //    return RedirectToAction("Index");
        //}

        //[AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        //public ActionResult Create(String name)
        //{
        //    Teams.addTeam(name);
        //    return RedirectToAction("Index");
        //}

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult AddEmployeeToTeam()
        {
            try
            {

                Models.EmpToTeamModel model = Models.EmpToTeamModel.Load();
                return View(model);

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        [HttpPost]
        public ActionResult AddEmployeeToTeam(Models.EmpToTeamModel model)
        {
            try
            {

                foreach (var t in model.Teams)
                    Teams.addEmployeesToTeam(model.Employees.ToArray(), t);

                if (model.SelectedEmployee != null)
                    return RedirectToAction("TeamsOfEmployee", "Teams", new { id = model.SelectedEmployee.Value });
                else if (model.SelectedTeam != null)
                    return RedirectToAction("Details", new { id = model.SelectedTeam.Value });
                else
                    return RedirectToAction("Index");


            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult Sort(String sort)
        {
            String old = (String)Session["TeamSort"];
            if (old.Contains(sort + "Asc"))
            {
                old = old.Replace((sort + "Asc"), "");
                old += sort;
                old += "Desc ";
            }
            else if (old.Contains(sort + "Desc"))
            {
                old = old.Replace((sort + "Desc"), "");
                old += sort;
                old += "Asc ";

            }
            else
            {
                old += sort;
                old += "Asc ";
            }
            Session["TeamSort"] = old;
            return RedirectToAction("Index", new { sort = old });
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult SortTeamsOfEmp(String sort, int empID, bool current)
        {
            String old = (String)Session["TeamsOfEmpSort"];
            if (old.Contains(sort + "Asc"))
            {
                old = old.Replace((sort + "Asc"), "");
                old += sort;
                old += "Desc ";
            }
            else if (old.Contains(sort + "Desc"))
            {
                old = old.Replace((sort + "Desc"), "");
                old += sort;
                old += "Asc ";

            }
            else
            {
                old += sort;
                old += "Asc ";
            }
            Session["TeamsOfEmpSort"] = old;
            return RedirectToAction("TeamsOfEmployee", new { id = empID, current = current });
        }

    }
}
