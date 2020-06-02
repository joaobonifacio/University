using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using University_II.Models;

namespace University_II.Services
{
    public class RoleService : IService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        List<T> IService.ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public bool CheckIfRoleHasBeenCreated(string userRole)
        {
            if (db.Roles.ToList().Count() == 0)
            {
                return false;
            }
            
            List<IdentityRole> rolesList = db.Roles.ToList();

            foreach (IdentityRole role in rolesList)
            {
                if (role.Name == userRole)
                {
                    return true;
                }
            }

            return false;
        }
    }
}