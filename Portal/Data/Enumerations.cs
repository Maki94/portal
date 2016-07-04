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
            FRator
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

        public enum CompanyField
        {
            IT,
            Bank,
            AutoIndustrija
        }

        public enum CompanyType
        {
            money,
            stuff,
        }

        public enum CommentType
        {
            Classic,
            HappyNews,
            SadNews,
            ImportantInfo
        }

        public enum TeamRole
        {
            MO,     //Main Organiser                        project || event

            FR,     //Fundraising Responsible               project || EB

            MR,     //Media Responsible                     project
            DR,     //Design Responsible                    project

            PR,     //Public Responsible                    EB                  ||
                    //Participants Responsible              project-course

            IT,     //Information Technologies Responsible  project

            LOG,    //Logistic Responsible                  project

            AR,     //Academia Responsible                  project-course
            
            Coworker,                                     //event

            Contributor,                                  //project || event

            President,                                    //EB
            Treasurer,                                    //EB
            Secretary,                                    //EB
        }

        public enum LoginStatus
        {
            Successful,
            Failed,
            IncorrectPassword
        }
        
    }
}
