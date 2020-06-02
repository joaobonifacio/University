using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.Services
{
    public class StaffService : IService
    { 
        ApplicationDbContext db = new ApplicationDbContext();

        public List<T> ListAll<T>()
        {
            throw new NotImplementedException();
        }


        public string CheckIfIsStaff(ApplicationUser user)
        {
            string staffMemberTitle;
            
            IEnumerable<StaffMember> staffMembers = db.StaffMembers.ToList();

            foreach (StaffMember staffMember in staffMembers)
            {
                if (user.Email == staffMember.Email)
                {
                    staffMemberTitle = staffMember.Title;

                    return staffMemberTitle;
                }
            }

            return null;
        }

        public StaffMember GetStaffMemberByID(int id)
        {
            StaffMember staff = db.StaffMembers.Find(id);

            if (staff == null)
                return null;

            return staff;
        }

        public List<StaffMember> GetAllStaffMembers()
        {
            return db.StaffMembers.ToList();
        }

        public StaffMember AddTeacherToStaffMember(Teacher teacher)
        {
            StaffMember staff = new StaffMember()
            {
                Email = teacher.Email,
                Title = "Teacher"
            };

            db.StaffMembers.Add(staff);
            db.SaveChanges();

            return staff;
        }

        public void DeleteTeacherFromStaffById(int id)
        {
            List<Teacher> teacher = db.Teachers.Where(t => t.Id == id).ToList();
            string teacherEmail = teacher.ToArray()[0].Email;

            List<StaffMember> staffMembers = db.StaffMembers
                .Where(s => s.Email == teacherEmail).ToList();

            StaffMember staff = staffMembers.ToArray()[0];
            db.StaffMembers.Remove(staff);
            db.SaveChanges();
        }
    }
}