using Data;
using Data.DTOs;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Poll
{
    public class PollListViewModel
    {
        public List<PollModel> ActivePolls { get; set; }
        public List<PollModel> ClosedPolls { get; set; }

        public PollListViewModel()
        {
            List<PollModel> pm = PollModel.GetAllPollModels();
            ActivePolls = pm.Where(p => p.PollDetails.State == Enumerations.PollState.aktivan).ToList();
            ClosedPolls = pm.Where(p => p.PollDetails.State == Enumerations.PollState.zatvoren).ToList();
        }
    }
}