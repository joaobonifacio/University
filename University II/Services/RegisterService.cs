using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.Services
{
    public class RegisterService : Services.IService
    {
        private UniversityStudentsListService uniStudentsListService;
        private StaffService staffService;

        List<T> IService.ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public string CheckTypeOfUserAndRegisterIt(ApplicationUser user)
        {
            String isStaff;
            String isStudent;

            staffService = new StaffService();
            uniStudentsListService = new UniversityStudentsListService();

            isStudent = uniStudentsListService.CheckIfIsStudentAndRegisterIt(user);

            if (isStudent != null)
            {
                return isStudent;
            }
            else
            {
                isStaff = staffService.CheckIfIsStaff(user);

                if (isStaff != null)
                {
                    return isStaff;
                }
            }

            return null;
        }
    }
}