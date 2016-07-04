using Data.DataClasses;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Polls
    {
        // od najnovije kreiranih ka starijim sortira
        public static List<Poll> GetAllPolls(DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return (from p in dc.Polls select p).OrderByDescending(p => p.StartDate).ToList();
            }
        }

        public static Poll GetPollAt(int pollId, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return (from p in dc.Polls where p.PollId == pollId select p).Include(p => p.PollCreator).First();
            }
        }

        public static Poll GetPollForPollOption(int pollOptionId, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return (from p in dc.PollOptions where p.PollOptionId == pollOptionId select p.Poll).FirstOrDefault();
            }
        }

        public static List<PollOption> GetPollOptionsForPoll(int pollId, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return (from p in dc.PollOptions where p.PollId == pollId select p).ToList();
            }
        }

        public static List<Poll> GetPollsCurrentAtDate(DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return (from p in dc.Polls where p.State == Enumerations.PollState.Aktivno select p).ToList();
            }
        }

        public static List<Member> GetVotersForPollOption(int pollOptionId, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return (from m in dc.MemberPollOptions where m.PollOptionId == pollOptionId select m.Member).ToList();
            }
        }


        public static void UpdatePollState(int pollId, Enumerations.PollState newState, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                Poll poll = (from p in dc.Polls where p.PollId == pollId select p).First();
                poll.State = newState;
                dc.SaveChanges();
            }
        }

        public static void ClosePoll(int pollId, DataContext dc = null)
        {
            UpdatePollState(pollId, Enumerations.PollState.Zatvoreno, dc);
        }

        public static void AddPoll(string topic, string description, bool multipleAnswers, bool hideResults, bool hideVoters, 
                                   DateTime endDate, string endTime, List<string> options, int creatorId, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                // u time smestimo HH:MM:SS
                var timeStrings = endTime.Split(':');
                TimeSpan ts = new TimeSpan(Int32.Parse(timeStrings[0]), Int32.Parse(timeStrings[1]), Int32.Parse(timeStrings[2]));
                DateTime endDateAndTime = endDate.Date + ts;
                Member creator = Members.GetMemberAt(creatorId);

                Poll poll = new Poll
                {
                    Topic = topic,
                    Description = description,
                    AllowMultiple = multipleAnswers,
                    HideResultsUntilFinished = hideResults,
                    HideVoters = hideVoters,
                    EndDate = endDateAndTime,
                    StartDate = DateTime.Now,
                    PollCreator = creator,
                    State = Enumerations.PollState.Aktivno
                };

                dc.Polls.Add(poll);

                List<PollOption> pollOptions = new List<PollOption>();

                foreach (var po in options)
                {
                    if (!string.IsNullOrWhiteSpace(po))
                    {
                        pollOptions.Add(new PollOption()
                        {
                            Answer = po,
                            Poll = poll
                        });
                    }
                }

                dc.PollOptions.AddRange(pollOptions);

                dc.SaveChanges();
            }
        }

        public static void AddVote(int memberId, int[] pollOptionIds, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                Member m = dc.Members.Where(x => x.MemberId == memberId).First();
                foreach (var poId in pollOptionIds)
                {
                    PollOption po = dc.PollOptions.Where(x => x.PollOptionId == poId).First();
                    MemberPollOption newVote = new MemberPollOption
                    {
                        Member = m,
                        PollOption = po,
                    };

                    dc.MemberPollOptions.Add(newVote);
                }

                dc.SaveChanges();
            }
        }

        public static bool RemoveVote(int memberId, int  pollOptionId, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                var removeVote = dc.MemberPollOptions.Where(x => x.MemberId == memberId && x.PollOptionId == pollOptionId).First();
                var voteRemoved = dc.MemberPollOptions.Remove(removeVote);

                dc.SaveChanges();
                return removeVote == voteRemoved;
            }
        }

        public static bool CheckIfMemberVotedInPoll (int memberId, int pollId)
        {
            using (var dc = new DataContext())
            {
                List<int> po = GetPollOptionsForPoll(pollId).Select(x => x.PollOptionId).ToList();
                return dc.MemberPollOptions.Where(x => x.MemberId == memberId && po.Contains(x.PollOptionId)).Any();
            }
        }
    }
}
