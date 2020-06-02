using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.Services.API
{
    public class StudentSubjectsService
    {
        private StudentSubjectService studentSubjectService;

        public List<StudentSubject> GetAllStudentSubjects()
        {
            studentSubjectService = new StudentSubjectService();

            return studentSubjectService.GetAllStudentSubjects();
        }

        public StudentSubject GetStudentSubject(int id)
        {
            studentSubjectService = new StudentSubjectService();

            return studentSubjectService.GetStudentSubjectByStudentSubjectId(id);
        }

        public StudentSubject UpdateStudentService(int id, StudentSubject studentSubject)
        {
            studentSubjectService = new StudentSubjectService();

            StudentSubject newStudentSubject = studentSubjectService.ChangeGrade(id, studentSubject);

            return newStudentSubject;
        }
    }
}