using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(MVC.Startup))]
namespace MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            using (Data.DataContext dc = new Data.DataContext())
            {
                Data.Entities.Members.AddMember("zantsu", "Milos", "Jajac",
                                                "jajac", "car", DateTime.Now);
            }
        }
    }
}
