using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Enumerations
    {
        public enum OwnerType
        {
            member,
            company,
        }

        public enum MemberStatus
        {
            pocetnik,
            bitan,
            kralj,
            car,
        }

        public enum PollState
        {
            aktivan,
            zatvoren,
        }

        public enum EventState
        {
            aktivan,
            zatvoren,
        }

        public enum ProjectState
        {
            aktivan,
            zatvoren,
        }
    }
}
