using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Enumerations
    {
        public enum Role
        {
            Clan = 1,
            Blagajnik,
            Sekretar,
            HR,
            FR,
            Administrator,
        }

        public enum Permission
        {
            ViewAboutPage = 1,
            AddMember,
            UploadDefaultPicture,
        }

        public enum OwnerType
        {
            member,
            company,
        }

        public enum FeedbackType
        {
            dobro,
            lose,
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

        public enum CompanyType
        {
            money,
            stuff,
        }

        public enum TeamRole
        {
            role1,
            role2,
        }

        public enum LoginStatus
        {
            Successful,
            Failed,
            IncorrectPassword
        }
    }
}
