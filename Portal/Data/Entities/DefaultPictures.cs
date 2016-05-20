using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class DefaultPictures
    {
        public static void UploadPicture(byte[] file, string name)
        {
            using (var dc = new DataContext())
            {
                DefaultPicture dp = new DefaultPicture
                {
                    Name = name,
                    Image = file
                };
                dc.DefaultPictures.Add(dp);
                dc.SaveChanges();
            }
        }

        public static byte[] GetPictureByName(string name)
        {
            using (var dc = new DataContext())
            {
                return (from p in dc.DefaultPictures
                        where p.Name == name
                        select p).First().Image;
            }
        }
    }
}
