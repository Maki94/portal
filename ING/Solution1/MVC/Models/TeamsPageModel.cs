using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2;

namespace MVC.Models
{
    public class TeamsPageModel
    {
       public List<Data2.DTOs.TeamDTO> List { get; set; }
    

        public String newTeam  { get; set; }

        public static TeamsPageModel Load()
        {
            TeamsPageModel model = new TeamsPageModel();
            model.newTeam = null;
            model.List = new List<Data2.DTOs.TeamDTO>();
           
            List<Team> teams;

            teams = Teams.getAllTeams().OrderByDescending(m => m.creationDate).ToList();
          
                
            foreach (var t in teams)
                model.List.Add(createTeamDTO(t));

            string sort = (String)HttpContext.Current.Session["TeamSort"];

            if (sort != null)
            {
                string[] sortList = sort.Split(' ');
                sortList.Reverse();
                foreach (string s in sortList)
                {
                    switch (s)
                    {                        

                        case "NameAsc":
                             model.List =  model.List.OrderBy(m => m.TeamName).ToList();
                            break;
                        case "NameDesc":
                             model.List =  model.List.OrderByDescending(m => m.TeamName).ToList();
                            break;

                        case "DateOfCreationAsc":
                             model.List =  model.List.OrderBy(m => m.DateOfCreation).ToList();
                            break;
                        case "DateOfCreationDesc":
                             model.List =  model.List.OrderByDescending(m => m.DateOfCreation).ToList();
                            break;

                        case "DateOfDeletionAsc":
                            model.List = model.List.OrderBy(m => m.DateOfDeletion).ToList();
                            break;
                        case "DateOfDeletionDesc":
                            model.List = model.List.OrderByDescending(m => m.DateOfDeletion).ToList();
                            break;
                    }
                }
            }
            return model;
        } 

        public static Data2.DTOs.TeamDTO createTeamDTO(Team t)
        {
            Data2.DTOs.TeamDTO team = new Data2.DTOs.TeamDTO
            {
                TeamID = t.teamID,
                TeamName = t.teamName,
                DateOfCreation = t.creationDate,
                DateOfDeletion = t.deleteDate
           
            };
            return team;
        }
    }
}