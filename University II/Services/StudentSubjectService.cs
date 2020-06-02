using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using University_II.Migrations;
using University_II.Models;

namespace University_II.Services
{
    public class StudentSubjectService : IService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private StudentService studentService;
        private CourseSubjectService courseSubjectService;
        private GradeService gradeService;
        private SubjectService subjectService;
        private StudentSubjectService studentSubjectService;
        

        public List<T> ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public List<StudentSubject> GetAllStudentSubjects()
        {
            return db.StudentSubjects.ToList();
        }

        public StudentSubject SaveStudentSubjectGrade(StudentSubject studentSubject)
        {
            var studentSubjectInDB = db.StudentSubjects.Single
                (s => s.StudentSubjectID == studentSubject.StudentSubjectID);

            if (studentSubject.Grade == Grade.A)
            {
                studentSubjectInDB.Grade = Grade.A;
            }
            else if (studentSubject.Grade == Grade.B)
            {
                studentSubjectInDB.Grade = Grade.B;
            }
            else if (studentSubject.Grade == Grade.C)
            {
                studentSubjectInDB.Grade = Grade.C;
            }
            else if (studentSubject.Grade == Grade.D)
            {
                studentSubjectInDB.Grade = Grade.D;
            }

            db.SaveChanges();

            return studentSubjectInDB;
        }

        public IEnumerable<StudentSubject> GetEnrolledStudentsStudentSubjects()
        {
            List<StudentSubject> allStudentSubjects = GetAllStudentSubjects();

            IEnumerable<StudentSubject> enrolledStudentsSTudentSubjects = from s in db.Students.ToList()
                                                                          join ss in allStudentSubjects
                                                                          on s.isEnrolled equals true
                                                                          select ss;
            return enrolledStudentsSTudentSubjects;
        }

        public StudentSubject ChangeGrade(int id, StudentSubject studentSubject)
        {
            gradeService = new GradeService();

            if (gradeService.ConvertGradeToInt(studentSubject.Grade -2 ) < 0 ||
                gradeService.ConvertGradeToInt(studentSubject.Grade -2) > 4)
                return null;

            StudentSubject newStudentSubject = db.StudentSubjects.Find(id);
            
            if(newStudentSubject == null)
                return null;

            newStudentSubject.Grade = studentSubject.Grade;
            db.SaveChanges();

            return newStudentSubject;
        }

        public void CreateStudentSubjectList(List<Subject> subjectsList, Student student)
        {
            List<StudentSubject> studentSubjects = new List<StudentSubject>();

            foreach (Subject subject in subjectsList)
            {
                StudentSubject studentSubject = new StudentSubject()
                {
                    StudentID = student.ID,
                    SubjectID = subject.ID,
                };

                db.StudentSubjects.Add(studentSubject);
                db.SaveChanges();
            }

            //testing the db

            List<StudentSubject> testStudentSubjectList = db.StudentSubjects.ToList();
            int count = 0;

            foreach (StudentSubject whatever in testStudentSubjectList)
            {
                if (whatever.StudentID == student.ID)
                {
                    count++;
                }
            }
        }

        public Grade? GetStudentAverageGradePerCourse(Course course)
        {
            studentService = new StudentService();
            subjectService = new SubjectService();
            studentSubjectService = new StudentSubjectService();
            gradeService = new GradeService();

            int studentsAverageGrade;
            int studentsAverageRounded;
            Grade? averageGrade;

            List<Subject> subjectsFromACourse = subjectService.GetSubjectsFromACourse(course);

            int numberOfSubjectsInACourse = subjectsFromACourse.Count();

            int numberOfStudentsRegisteredInACourse = studentService
                    .GetStudentsRegisteredInACourse(course).Count();

            int studentsTotalGrades = studentSubjectService
                .GetTotalStudentGradesFromACourse(course);

            int numberOfStudentSubjectsGraded = GetNumberOfStudentsSubjectsGradedInACourse(subjectsFromACourse);


            if (numberOfSubjectsInACourse != 0 && numberOfStudentSubjectsGraded != 0)
            {
                studentsAverageGrade =
                studentsTotalGrades / numberOfStudentSubjectsGraded;
            }
            else studentsAverageGrade = 0;

            studentsAverageRounded = (int)Math.Round((decimal)studentsAverageGrade);

            averageGrade = gradeService.ConvertIntToGrade(studentsAverageRounded);


            return averageGrade;
        }

        public void DeleteStudentSubjectById(int id)
        {
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            db.StudentSubjects.Remove(studentSubject);
            db.SaveChanges();
        }

        public List<Subject> GetSubjectsByStudentId(int Id)
        {
            IEnumerable<Subject> subjects = from ss in db.StudentSubjects
                                            join s in db.Students
                                            on ss.StudentID equals Id
                                            select ss.Subject;

            return subjects.ToList();
        }

        public int GetNumberOfStudentsSubjectsGradedInACourse(List<Subject> subjectsFromACourse)
        {
            IEnumerable<StudentSubject> studentSubjectsGraded = from s in subjectsFromACourse
                                                         join ss in db.StudentSubjects
                                                         on s.ID equals ss.SubjectID
                                                         where ss.Grade != null
                                                         where ss.Student.isEnrolled
                                                         select ss;

            return studentSubjectsGraded.ToList().Count;
        }

        public List<StudentSubject> GetStudentSubjectsByStudentId(int studentId)
        {
            IEnumerable<StudentSubject> studentSubjects = db.StudentSubjects.ToList();
            List<StudentSubject> studentSubjectsToAddList = new List<StudentSubject>();

            foreach (StudentSubject studentSubject in studentSubjects)
            {
                if (studentSubject.StudentID == studentId)
                {
                    studentSubjectsToAddList.Add(studentSubject);
                }
            }

            return studentSubjectsToAddList;
        }

        public List<StudentSubject> GetAllStudentSubjectsRelatedToASubject(int subjectID)
        {
            List<StudentSubject> studentSubjects = db.StudentSubjects.ToList();
            List<StudentSubject> studentSubjectsToReturn = new List<StudentSubject>();

            foreach (StudentSubject studentSubject in studentSubjects)
            {
                if(studentSubject.SubjectID == subjectID)
                {
                    studentSubjectsToReturn.Add(studentSubject);
                }
            }

            return studentSubjectsToReturn;
        }

        public int NumberOfStudentsPerSubject(Subject subject)
        {
            int numberOfStudents = 0;
            IEnumerable<StudentSubject> studentSubjects = FindStudentSubjectsBySubject(subject);
            studentService = new StudentService();

            foreach (StudentSubject studentSubject in studentSubjects)
            {
                Student student = studentService.GetStudentByID(studentSubject.StudentID);

                if (student != null && student.isEnrolled)
                {
                    numberOfStudents++;
                }
            }

            return numberOfStudents;
        }

        public int GetTotalStudentGradesFromACourse(Course course)
        {
            courseSubjectService = new CourseSubjectService();
            gradeService = new GradeService();
            studentService = new StudentService();

            List<Student> studentsRegistered = studentService.GetStudentsRegisteredInACourse(course);

            IEnumerable<StudentSubject> registeredStudentsSubjects = from s in studentsRegistered
                                                              join ss in db.StudentSubjects
                                                              on s.ID equals ss.StudentID
                                                              select ss;
            
            int sumOfGrades = 0;

            foreach (StudentSubject studentSubject in registeredStudentsSubjects.ToList())
            {
                if (studentSubject.Student.isEnrolled && studentSubject.Grade != null)
                {
                    sumOfGrades = sumOfGrades + gradeService.ConvertGradeToInt(studentSubject.Grade);
                }
            }

            return sumOfGrades;
        }

        public List<StudentSubject> GetStudentSubjectListFromALIstOfSubjects(List<Subject> subjects)
        {
            List<StudentSubject> studentSubjects = new List<StudentSubject>();

            foreach (StudentSubject studentSubject in db.StudentSubjects.ToList())
            {
                foreach (Subject subject in subjects)
                {
                    if (studentSubject.SubjectID == subject.ID)
                    {
                        studentSubjects.Add(studentSubject);
                    }
                }
            }

            return studentSubjects;
        }

        public IEnumerable<StudentSubject> FindStudentSubjectsBySubject(Subject subject)
        {
             IEnumerable<StudentSubject> studentsubjects = db.StudentSubjects.ToList();
             List<StudentSubject> studentSubjectsList = new List<StudentSubject>();

             foreach (StudentSubject studentSubject in studentsubjects)
             {
                 if (studentSubject.SubjectID == subject.ID)
                 {
                     studentSubjectsList.Add(studentSubject);
                 }
             }

             return studentSubjectsList;
        }

        public void RemoveStudentsStudentSubjectsByStudentId(int id)
        {
            Student student = db.Students.Find(id);

            List<StudentSubject> thisStudentSubjects = db.StudentSubjects
                .Where(ss => ss.StudentID == id).ToList();

            foreach(StudentSubject studentSubject in thisStudentSubjects)
            {
                db.StudentSubjects.Remove(studentSubject);
                db.SaveChanges();
            }
        }

        public void SaveGrade(StudentSubject studentSubject)
        {
            StudentSubject theStudentSubject = db.StudentSubjects.Find(studentSubject.StudentSubjectID);
            theStudentSubject.Grade = studentSubject.Grade;

            db.SaveChanges();
        }

        public Grade? GetStudentAverageGrade(int id)
        {
            gradeService = new GradeService();

            IEnumerable<Grade?> totalGrades = from ss in db.StudentSubjects.ToList()
                                              join s in db.Students.ToList()
                                              on ss.StudentID equals s.ID
                                              where s.isEnrolled 
                                              where s.ID == id
                                              select ss.Grade;

            List<Grade?> realTotalGrades = new List<Grade?>();

            foreach(Grade? grade in totalGrades)
            {
                if(grade.HasValue)
                {
                    realTotalGrades.Add(grade);
                }
            }

            int totalGradesInInt = gradeService.ConvertAllGradesToInt(totalGrades.ToList());

            if(realTotalGrades.ToList().Count != 0)
            {
                decimal average = totalGradesInInt / realTotalGrades.ToList().Count;

                int roundedAverage = (int)Math.Round(average);

                Grade? averageInGrade = gradeService.ConvertIntToGrade(roundedAverage);
            }

            return null;
        }

        public StudentSubject GetStudentSubjectByStudentSubjectId(int Id)
        {
            StudentSubject studentSubject = new StudentSubject();
            studentSubject = db.StudentSubjects.Find(Id);

            if(studentSubject != null)
            {
                return studentSubject;
            }

            return null;
        }

        public void RemoveStudentsStudentSubjects(Student student)
        {
            IEnumerable<StudentSubject> studentSubjectsToRemove = new List<StudentSubject>();

            studentSubjectsToRemove = from s in db.Students
                                      join ss in db.StudentSubjects
                                      on s.ID equals ss.StudentID
                                      select ss;

            foreach(StudentSubject subject in studentSubjectsToRemove.ToList())
            {
                db.StudentSubjects.Remove(subject);
                db.SaveChanges();
            }
        }

        public IEnumerable<StudentSubject> GetStudentSubjectsByTeacherId(int id)
        {
            IEnumerable<Subject> teachersSubjects = new List<Subject>();

            teachersSubjects = from t in db.Teachers
                              join s in db.Subjects
                              on t.Id equals s.TeacherId
                               where t.Id == id
                               select s;

            IEnumerable<StudentSubject> teachersStudentSubjects = new List<StudentSubject>();

            teachersStudentSubjects = from t in teachersSubjects
                                      join ss in db.StudentSubjects
                                      on t.ID equals ss.SubjectID
                                      select ss;

            return teachersStudentSubjects;
        }

    }
}