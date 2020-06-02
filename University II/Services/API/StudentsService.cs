using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using University_II.Controllers.API;
using University_II.Models;
using University_II.Models.API;

namespace University_II.Services.API
{
    public class StudentsService
    {
        StudentService studentService;
        StudentSubjectService studentSubjectService;
        StudentRegisterService studentRegisterService;

        public List<StudentToExpose> GetAllEnrolledStudents()
        {
            studentService = new StudentService();

            List<StudentToExpose> studentsToExpose = new List<StudentToExpose>();

            List<Student> allStudents = studentService.GetAllEnrolledStudents();

            foreach (Student student in allStudents)
            {
                StudentToExpose studentToExpose = new StudentToExpose()
                {
                    Id = student.ID,
                    Name = student.Name,
                    Email = student.Email,
                    CourseId = student.CourseId,
                    IsEnrolled = student.isEnrolled
                };

                studentsToExpose.Add(studentToExpose);
            }

            return studentsToExpose;
        }

        public Student GetStudent(int id)
        {
            studentService = new StudentService();

            return studentService.GetStudentByID(id);
        }

        public Student RegisterStudent(UniversityStudentsList uniStudent)
        {
            studentRegisterService = new StudentRegisterService();

            Student student = studentRegisterService.RegisterStudent(uniStudent);

            return student;
        }

        public bool  CheckIfStudentExistsById(int id)
        {
            studentService = new StudentService();

            return studentService.CheckIfStudentExistsById(id);
        }

        public Student UpdateStudent(int id, Student student)
        {
            studentService = new StudentService();

            return studentService.UpdateStudentById(id, student);
        }

        public void DeleteStudent(int id)
        {
            studentService = new StudentService();

            studentService.DeleteStudentByStudentId(id);
        }
    }
}