using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.DTOs;
using Data.Entities;

namespace MVC.Models
{
    public class PollModel
    {
        public PollDTO PollDetails { get; set; }
        public List<PollOptionDTO> PollOptions { get; set; }
        public bool AlreadyVoted { get; set; }

        public static PollModel Load(int pollId)
        {
            var model = new PollModel
            {
                PollDetails = CreatePollDTO(pollId),
                PollOptions = CreatePollOptionDTOs(pollId)
            };

            // ovo ce da zatvori glasanje ako mu je proslo vreme
            model = CheckUpdatePollState(model);

            model.AlreadyVoted = Polls.CheckIfMemberVotedInPoll(MemberSession.GetMemberId(), pollId);

            return model;
        }

        public static PollDTO CreatePollDTO(int pollId)
        {
            var p = Polls.GetPollAt(pollId);
            var pdto = new PollDTO
            {
                PollId = p.PollId,
                Topic = p.Topic,
                Description = p.Description,
                AllowMultiple = p.AllowMultiple,
                HideResultsUntilFinished = p.HideResultsUntilFinished,
                HideVoters = p.HideVoters,
                State = p.State,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                PollCreatorName = p.PollCreator.Name,
            };
            return pdto;
        }

        public static List<PollOptionDTO> CreatePollOptionDTOs(int pollId)
        {
            var pollOptions = Polls.GetPollOptionsForPoll(pollId);
            var pollOptionDTOs = new List<PollOptionDTO>();
            foreach (var p in pollOptions)
            {
                List<int> voterIds = Polls.GetVotersForPollOption(p.PollOptionId).Select(x => x.MemberId).ToList();

                pollOptionDTOs.Add(new PollOptionDTO
                {
                    PollOptionId = p.PollOptionId,
                    Answer = p.Answer,
                    PollId = p.PollId,
                    VotersIds = voterIds
                });
            }

            return pollOptionDTOs.ToList();
        }

        public static List<PollModel> GetAllPollModels()
        {
            var polls = Polls.GetAllPolls();
            var pollModels = new List<PollModel>();
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
                Polls.ClosePoll(pm.PollDetails.PollId);
                // menjamo vrednost propertija objekta
                pm.PollDetails.State = Enumerations.PollState.Zatvoreno;
            }

            return pm;
        }
    }
}