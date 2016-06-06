using Data;
using Data.DataClasses;
using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class PollModel
    {
        public PollDTO PollDetails { get; set; }
        public List<PollOptionDTO> PollOptions { get; set; }

        public static PollModel Load(int pollId)
        {
            PollModel model = new PollModel
            {
                PollDetails = CreatePollDTO(pollId),
                PollOptions = CreatePollOptionDTOs(pollId),
            };

            // ovo ce da zatvori glasanje ako mu je proslo vreme
            model = CheckUpdatePollState(model);

            return model;
        }

        public static PollDTO CreatePollDTO(int pollId)
        {
            Poll p = Polls.GetPollAt(pollId);
            PollDTO pdto = new PollDTO
            {
                Id = p.PollId,
                Topic = p.Topic,
                Description = p.Description,
                MaxAnswers = p.MaxAnswers,
                State = p.State,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                PollCreator = p.PollCreator,
            };
            return pdto;
        }

        public static List<PollOptionDTO> CreatePollOptionDTOs(int pollId)
        {
            List<PollOption> pollOptions = Polls.GetPollOptionsForPoll(pollId);
            List<PollOptionDTO> pollOptionDTOs = new List<PollOptionDTO>();
            foreach (var p in pollOptions)
            {
                pollOptionDTOs.Add(new PollOptionDTO
                {
                    Id = p.PollOptionId,
                    Answer = p.Answer,
                    Voters = Polls.GetVotersForPollOption(p.PollOptionId),
                });
            }

            return pollOptionDTOs;
        }

        public static List<PollModel> GetAllPollModels()
        {
            List<Poll> polls = Polls.GetAllPolls();
            List<PollModel> pollModels = new List<PollModel>();
            foreach (var p in polls)
            {
                pollModels.Add(Load(p.PollId));
            }

            return pollModels;
        }


        public static PollModel CheckUpdatePollState(PollModel pm)
        {
            if (pm.PollDetails.EndDate < DateTime.Now)
            {
                // pozivamo funkciju koja ce da updatuje bazu
                Polls.ClosePoll(pm.PollDetails.Id);
                // menjamo vrednost propertija objekta
                pm.PollDetails.State = Enumerations.PollState.zatvoren;
            }

            return pm;
        }
    }
}