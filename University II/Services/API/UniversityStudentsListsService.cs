using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using University_II.Controllers.API;
using University_II.Models;

namespace University_II.Services.API
{
    public class UniversityStudentsListsService
    {
        private UniversityStudentsListService universityStudentsListService;
        private StudentService studentService;

        public List<UniversityStudentsList> GetAllUniversityStudentsLists()
        {
            universityStudentsListService = new UniversityStudentsListService();

            return universityStudentsListService.GetUniversityStudentsLists();
        }

        public UniversityStudentsList GetUniversityStudentsList(int id)
        {
            universityStudentsListService = new UniversityStudentsListService();

            return universityStudentsListService.GetUniversityStudentsList(id);
        }

        public UniversityStudentsList CreateUniversityStudent(UniversityStudentsList uniStudent)
        {
            universityStudentsListService = new UniversityStudentsListService();

            UniversityStudentsList universityStudent = universityStudentsListService
                .CreateUniversityStudentList(uniStudent);

            return universityStudent;
        }

        public Student CheckIfUniversityStudentExistsInStudentsDB(UniversityStudentsList uniStudent)
        {
            universityStudentsListService = new UniversityStudentsListService();

            Student student = universityStudentsListService
                           .CheckIfUniversityStudentExistsInStudentsDB(uniStudent);

            return student;
        }

        public UniversityStudentsList UpdateUniversityStudent(int id, UniversityStudentsList uniStudent)
        {
            universityStudentsListService = new UniversityStudentsListService();

            UniversityStudentsList universityStudent = universityStudentsListService
                .UpdateUniversityStudent(id, uniStudent);

            return universityStudent;
        }

        public bool CheckIfStudentExistsInUniversityListDb(int id)
        {
            universityStudentsListService = new UniversityStudentsListService();

            return universityStudentsListService.CheckIfStudentExistsInUniversityListDb(id);
        }
        

        public void UpdateStudentByUniStudent(UniversityStudentsList previousUniStudent, Student student)
        {
            studentService = new StudentService();

            studentService.UpdateStudentByUniStudent(previousUniStudent, student);
        }
    }
}