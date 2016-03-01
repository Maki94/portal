using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2
{
    public class RolesPermissions
    {
        public List<List<int>> rolPerm = new List<List<int>>();
        public int NumOfRoles { get; set; }


        public static RolesPermissions Load()
        {
            RolesPermissions rP = new RolesPermissions();
            List<int> empPerm = new List<int> { 4, 7, 8 };
            List<int> manPerm = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

            rP.rolPerm.Add(empPerm);
            rP.rolPerm.Add(manPerm);            
            rP.NumOfRoles = 2;
            return rP;
        }

        public static void addRolesPermisons()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            RolesPermissions rP = RolesPermissions.Load();
            for (int i = 0; i < rP.NumOfRoles; i++)
            {
                foreach (int j in rP.rolPerm[i])
                {
                    RolesPermission rolesPermission = new RolesPermission
                    {
                        roleID = i + 1,
                        premissionID = j,
                    };
                    dc.RolesPermissions.InsertOnSubmit(rolesPermission);
                }
            }
            dc.SubmitChanges();

        }

    }
}
