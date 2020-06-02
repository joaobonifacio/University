using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Controllers.API;
using University_II.Models;
using University_II.ViewModels;

namespace University_II.Services
{
    public class StudentService : IService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UniversityStudentsListService uniService;

        public List<Student> GetAllEnrolledStudents()
        {
            List<Student> allStudents =  db.Students.ToList();

            return allStudents;
        }

        public List<T> ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public Student AddStudent(UniversityStudentsList student)
        {
            uniService = new UniversityStudentsListService();

            Student studentToRegister = new Student()
            {
                Name = student.Name,
                Email = student.Email,
                CourseId = student.CourseId,
                isEnrolled = true
            };

            db.Students.Add(studentToRegister);
            db.SaveChanges();

            uniService.UpdateUniversityStudentEnrollment(student);

            return studentToRegister;
        }


        public Student GetStudentByID(int studentId)
        {
            Student student = db.Students.Find(studentId);

            return student;
        }

        public IEnumerable<Student> GetStudentsByStudentSubjects(List<StudentSubject> studentSubjects)
        {
            IEnumerable<Student> students = from ss in studentSubjects
                                     join s in db.Students.ToList()
                                     on ss.StudentID equals s.ID
                                     select s;

            return students;
        }

        public List<CourseStudentSubjectViewModel> CreateCourseStudentSubjectViewModel(IEnumerable<Subject> subjects, IEnumerable<Course> courses, IEnumerable<Student> students, List<StudentSubject> studentSubjects)
        {
            List <CourseStudentSubjectViewModel>  viewModel = new List<CourseStudentSubjectViewModel>();

            for (int i = 0; i < studentSubjects.ToArray().Length; i++)
            {
                CourseStudentSubjectViewModel model = new CourseStudentSubjectViewModel()
                {
                    Subject = subjects.ToArray()[i],
                    Course = courses.ToArray()[i],
                    Student = students.ToArray()[i],
                    StudentSubject = studentSubjects.ToArray()[i]

                };

                viewModel.Add(model);
            }

            return viewModel;
        }

        public List<StudentMySubjectsViewModel> CreateStudentMySubjectsViewModel(Course studentCourse, Student theStudent, List<StudentSubject> studentSubjects)
        {
            List <StudentMySubjectsViewModel>  viewModel = new List<StudentMySubjectsViewModel>();

            foreach (StudentSubject studentSubject in studentSubjects)
            {
                StudentMySubjectsViewModel model = new StudentMySubjectsViewModel
                {
                    course = studentCourse,
                    student = theStudent,
                    studentSubject = studentSubject
                };

                viewModel.Add(model);
            }

            return viewModel;
        }

        public bool CheckIfStudentExistsById(int id)
        {
            Student student = db.Students.Find(id);

            if(student == null)
                return false;

            return true;
        }

        public Student UpdateStudentById(int id, Student student)
        {
            uniService = new UniversityStudentsListService();

            Student studentToUpdate = db.Students.Find(id);

            studentToUpdate.Name = student.Name;

            db.SaveChanges();

            // update uni student name
            uniService.ChangeNameByStudentId(studentToUpdate.Email, student.Name);

            return studentToUpdate;

        }

        public StudentAverageGradeViewModel CreateStudentAverageGradeViewModel(Student student, Course course, Grade? averageGrade)
        {

            StudentAverageGradeViewModel viewModel = new StudentAverageGradeViewModel()
            {
                student = student,
                course = course,
                grade = averageGrade
            };

            return viewModel;
        }

        public Student GetStudentByName(string email)
        {
            if(email == null)
            {
                return null;
            }
            List<Student> students = db.Students.Where(s => s.Email == email).ToList();

            if(students.Count() == 0)
            {
                return null;
            }

            Student student = students.ToArray()[0];

            return student;
        }

        public List<Student> GetStudentsRegisteredInACourse(Course course)
        {
            IEnumerable<Student> studentsInACourse = db.Students.Where(s => s.CourseId == course.Id);

            IEnumerable<Student> studentsRegisteredInACourse = studentsInACourse.Where(s => s.isEnrolled);


            return studentsRegisteredInACourse.ToList();
        }

        public void UpdateStudentByUniStudent(UniversityStudentsList previousUniStudent, Student student)
        {
            Student theStudent = db.Students.Find(student.ID);

            theStudent.Name = previousUniStudent.Name;
            //theStudent.CourseId = previousUniStudent.CourseId;

            db.SaveChanges();
        }

        public bool CheckIfStudentWasAlreadyAdded(List<Student> theStudents, Student student)
        {
            bool checkIfStudentWasAlreadyAdded = false;

            checkIfStudentWasAlreadyAdded = theStudents.Contains(student);
            
            return checkIfStudentWasAlreadyAdded;
        }

        public List<int> GetNumberOfStudentsEnrolledInASubject(List<Subject> allSubjects)
        {
            List<int> studentsEnrolled = new List<int>();
            int count = 0;

            foreach(Subject subject in allSubjects)
            {
                foreach(StudentSubject studentSubject in db.StudentSubjects.ToList())
                {
                    if(studentSubject.SubjectID == subject.ID)
                    {
                        if (studentSubject.Grade != null)
                        {
                            count++;
                        }
                    }
                }

                studentsEnrolled.Add(count);
                count = 0;
            }

            return studentsEnrolled;
        }

        public Course GetStudentCourse(int? id)
        {
            IEnumerable<Course> course = from s in db.Students.ToList()
                            join c in db.Courses.ToList()
                            on s.CourseId equals c.Id
                            select c;

            Course theCourse = course.ToArray()[0];

            return theCourse;
        }

        public void UpdateStudent(EditStudentViewModel editedStudent)
        {
            Student student = new Student();

            student = GetStudentByID(editedStudent.StudentId);
            student.Name = editedStudent.Name;
            student.Email = editedStudent.Email;

            db.SaveChanges();
        }

        public void DeleteStudentByStudentId(int Id)
        {
            Student student = db.Students.Find(Id);

            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }
    }
}