using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.Services
{
    public class StudentRegisterService: IService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CourseSubjectService courseSubjectService;
        private StudentService studentService;
        private StudentSubjectService studentSubjectService;
        private UniversityStudentsListService universityStudentsListService;

        public List<T> ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public Student RegisterStudent(UniversityStudentsList student)
        {
            courseSubjectService = new CourseSubjectService();
            studentService = new StudentService();
            studentSubjectService = new StudentSubjectService();
            universityStudentsListService = new UniversityStudentsListService();


            Student studentToRegister = new Student();

            // adding and saving student to db, changing university list isEnrolled to true
            studentToRegister = studentService.AddStudent(student); 

            //populate student with student subjects
            ICollection<Subject> studentCourseSubjects = new List<Subject>();

            studentCourseSubjects = courseSubjectService.GetSubjectsFromCourse(studentToRegister.CourseId);
            
            studentSubjectService.CreateStudentSubjectList(studentCourseSubjects.ToList(),
                            studentToRegister);
            
            return studentToRegister;
        }
    }
}