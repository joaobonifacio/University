using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;
using University_II.Models.API;

namespace University_II.Services.API
{
    public class StudentSubjectToExposeService
    {
        public List<StudentSubjectToExpose> TrimStudentSubjects(List<StudentSubject> studentSubjects)
        {
            List<StudentSubjectToExpose> studentSubjectsToExpose = new List<StudentSubjectToExpose>();

            foreach(StudentSubject studentSubject in studentSubjects)
            {
                StudentSubjectToExpose studentSubjectToExpose = new StudentSubjectToExpose()
                {
                    StudentSubjectID = studentSubject.StudentSubjectID,
                    SubjectID = studentSubject.SubjectID,
                    StudentID = studentSubject.StudentID,
                    grade = studentSubject.Grade + 1
                };

                studentSubjectsToExpose.Add(studentSubjectToExpose);
            }

            return studentSubjectsToExpose;
        }

        public StudentSubjectToExpose TrimStudentSubject(StudentSubject studentSubject)
        {
            StudentSubjectToExpose studentSubjectToExpose = new StudentSubjectToExpose()
            {
                StudentSubjectID = studentSubject.StudentSubjectID,
                SubjectID = studentSubject.SubjectID,
                StudentID = studentSubject.StudentID,
                grade = studentSubject.Grade
            };

            return studentSubjectToExpose;
        }
    }
}