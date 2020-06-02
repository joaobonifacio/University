using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.Services.API
{
    public class StaffsService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private StaffService staffService;

        public StaffMember AddTeacherToStaffMember(Teacher theTeacher)
        {
            staffService = new StaffService();

            StaffMember teacher = staffService.AddTeacherToStaffMember(theTeacher);

            return teacher;
        }

        public List<StaffMember> GetAllStaffMembers()
        {
            staffService = new StaffService();

            return staffService.GetAllStaffMembers();
        }

        public StaffMember GetStaffMemberByID(int id)
        {
            staffService = new StaffService();

            StaffMember staff = staffService.GetStaffMemberByID(id);

            return staff;
        }
    }
}