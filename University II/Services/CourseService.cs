using Microsoft.Owin.Security.Provider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using University_II.Models;
using University_II.Models.API;
using University_II.ViewModels;

namespace University_II.Services
{
    public class CourseService : IService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TeacherService teacherService;
        private SubjectService subjectService;
        private UniversityStudentsListService universityStudentsListService;
        private CourseSubject courseSubject;

        List<T> IService.ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public List<Course> ListCourses()
        {
            List<Course> courses = new List<Course>();
            courses = db.Courses.ToList();

            return courses;
        }

        public List<CourseSubject> CreateCourse(CourseCreator courseCreator)
        {
            courseSubject = new CourseSubject();
            
            Course course = new Course();
            course.Title = courseCreator.Title;

            db.Courses.Add(course);
            db.SaveChanges();

            List<int> subjectsIds = courseCreator.SubjectIds;
            List<CourseSubject> courseSubjects = new List<CourseSubject>();

            courseSubjects = courseSubject.CreateCourseSubjectsBySubjectsIDAndCourse(course, subjectsIds);
            
            return courseSubjects;
        }

        public  bool CheckIfCourseAlreadyExists(string title)
        {
            bool exists = false;
            IEnumerable<Course> courses = db.Courses.Where(c => c.Title == title);

            if (courses.Any())
                return true;

            return exists;
        }

        public Dictionary<Subject, Teacher> getSubjectTeacherPairs(int courseId)
        {
            Dictionary<Subject, Teacher> subjectTeacherPairs =
                new  Dictionary<Subject, Teacher>();
            
            IEnumerable<CourseSubject> allCourseSubjects = db.CourseSubjects.ToList();

            teacherService = new TeacherService();
            subjectService = new SubjectService();

            foreach (CourseSubject courseSubject in allCourseSubjects)
            {
                if (courseSubject.CourseId == courseId)
                {
                    Subject wantedSubject = subjectService
                        .getSubjectById(courseSubject.SubjectId);

                    Teacher wantedTeacher = teacherService
                        .getCorrespondingTeacher(wantedSubject.TeacherId);

                    subjectTeacherPairs.Add(wantedSubject, wantedTeacher);
                }
            }

            return subjectTeacherPairs;
        }

        public List<TeacherSubjectViewModel> CreateTeacherSubjectViewModel(int? id, Dictionary<Subject, Teacher> subjectTeacherPairs)
        {

            List <TeacherSubjectViewModel> viewModel = new List<TeacherSubjectViewModel>();

            Subject Subject = new Subject();
            Teacher teacher = new Teacher();
            Course course = db.Courses.Find(id);

            foreach (KeyValuePair<Subject, Teacher> item in subjectTeacherPairs)
            {
                TeacherSubjectViewModel model = new TeacherSubjectViewModel()
                {
                    Course = course,
                    Subject = item.Key,
                    Teacher = item.Value
                };

                viewModel.Add(model);
            }

            return viewModel;
        }

        public bool CheckIfCourseAlreadyExistsById(int id)
        {
            Course course = db.Courses.Find(id);

            if (course == null)
                return false;

            return true;
        }

        public Course UpdateTitle(int id, string title)
        {
            Course course = db.Courses.Find(id);

            if (course == null)
                return null;

            course.Title = title;
            db.SaveChanges();

            return course;
        }

        public List<int> GetCourseSubjectsIds(Course courseToUpdate)
        {
            IEnumerable<CourseSubject> courseSubjects = db.CourseSubjects
                .Where(c => c.CourseId == courseToUpdate.Id);

            if (courseSubjects.Count() == 0)
                return null;

            List<int> subjectIds = new List<int>();

            foreach(CourseSubject courseSubject in courseSubjects)
            {
                subjectIds.Add(courseSubject.SubjectId);
            }

            return subjectIds;
           
        }

        public Course GetCourseByTitle(string courseTitle)
        {
            IEnumerable<Course> courses = db.Courses.Where(c => c.Title == courseTitle);

            if (courses.ToList().Count() == 0)
                return null;

            return courses.ToArray()[0];
        }

        public void DeleteCourseById(int id)
        {
            if(id == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Course courseToDelete = db.Courses.Find(id);

            if(courseToDelete == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            RemoveCourse(courseToDelete);
            db.SaveChanges();
        }

        public Course ChangeCourseTitleById(int id, string title)
        {
            if(id == 0)
            {
                return null;
            }

            Course course = db.Courses.Find(id);

            if (course == null)
            {
                return null;
            }
            
            course.Title = title;
            db.SaveChanges();
            
            return course;      
        }

        public IEnumerable<Course> GetCoursesFromListOfStudentSubjects(List<StudentSubject> studentSubjects)
        {
            IEnumerable<Course> courses = from ss in studentSubjects
                                   join s in db.Students.ToList()
                                   on ss.StudentID equals s.ID
                                   join c in db.Courses.ToList()
                                   on s.CourseId equals c.Id
                                   select c;

                return courses;
        }

        public void UpdateCourse(Course course)
        {
            Course theCourse = db.Courses.Find(course.Id);

            if(theCourse != null)
            {
                theCourse.Title = course.Title;
                db.SaveChanges();
            }
        }

        public Course CreateCourseByName(string courseName)
        {
            Course course = new Course()
            {
                Title = courseName
            };

            db.Courses.Add(course);
            db.SaveChanges();

            return course;
        }

        public List<Course> RemoveCourse(Course course)
        {
            if(course != null)
            {
                db.Entry(course).State = EntityState.Deleted;
                db.SaveChanges();       
            }

            return db.Courses.ToList();
        }

        public Course GetStudentCourseByStudent(Student student)
        {
            int courseId = student.CourseId;

            Course course = db.Courses.Find(courseId);
            
            return course;
        }

        public Course GetCourseByCourseId(int id)
        {
            Course course = db.Courses.Find(id);

            if(course == null){
                return null;
            }

            return course;
        }
    }
}