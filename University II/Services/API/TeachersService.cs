using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.Services.API
{
    public class TeachersService
    {
        private TeacherService teacherService;
        private StaffService staffService;

        public List<Teacher> GetNonAllocatedTeachers()
        {
            teacherService = new TeacherService();
            
            return teacherService.GetNonAllocatedTeachers();
        }

        public List<Teacher> GetListJustOfTeachers()
        {
            teacherService = new TeacherService();

            return teacherService.GetListJustOfTeachers();  
        }

        public void DeleteTeacherById(int id)
        {
            teacherService = new TeacherService();
            teacherService.DeleteTeacherById(id);
        }

        public Teacher CreateNewTeacher(Teacher theTeacher)
        {
            teacherService = new TeacherService();
            return teacherService.CreateNewTeacher(theTeacher);
        }

        public Teacher GetTeacherByID(int id)
        {
            teacherService = new TeacherService();

            Teacher teacher = teacherService.getCorrespondingTeacher(id);

            return teacher;
        }

        public Teacher UpdateTeacher(int id, Teacher teacher)
        {
            teacherService = new TeacherService();
            
            return teacherService.UpdateTeacher(id, teacher);
        }

        public Teacher GetTeacherNameByID(int teacherId)
        {
            teacherService = new TeacherService();

            return teacherService.getCorrespondingTeacher(teacherId);
        }

        public bool CheckIfTeacherAlreadyExists(Teacher teacher)
        {
            teacherService = new TeacherService();

            return teacherService.CheckIfTeacherAlreadyExists(teacher);
        }

        public void AddTeacherToStaff(Teacher teacher)
        {
            staffService = new StaffService();

            staffService.AddTeacherToStaffMember(teacher);
        }

        public bool CheckIfTeacherHasSubject(int id)
        {
            teacherService = new TeacherService();

            return teacherService.CheckIfTeacherHasSubject(id);
        }

        public void DeleteTeacherAndSubstituteHim(int id)
        {
            teacherService = new TeacherService();

            // also removes him from staff
            teacherService.DeleteTeacherAndSubstituteHim(id);
        }
    }
}