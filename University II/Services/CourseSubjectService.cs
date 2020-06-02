using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using University_II.Models;
using University_II.ViewModels;

namespace University_II.Services
{
    public class CourseSubjectService: IService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private SubjectService subjectService;
        private TeacherService teacherService;
        private StudentService studentService;
        private StudentSubjectService studentSubjectService;
        private CourseSubjectService courseSubjectService;




        List<T> IService.ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public List<CourseSubject> GetAllCourseSubjects()
        {
            IEnumerable<CourseSubject> courseSubjects = db.CourseSubjects.Include(cs => cs.Course).Include(c => c.Subject).ToList();

            return courseSubjects.ToList();

        }

        public List<Subject> GetSubjectsFromCourse(int courseId)
        {
            List<Subject> studentSubjects = new List<Subject>();
            IEnumerable<CourseSubject> courseSubjects = db.CourseSubjects.ToList();
            Subject subject = new Subject();
            int subjectId;

            subjectService = new SubjectService();

            foreach (CourseSubject courseSubject in courseSubjects)
            {
                if (courseSubject.CourseId == courseId)
                {
                    subjectId = courseSubject.SubjectId;

                    subject = subjectService.getSubjectById(subjectId);

                    studentSubjects.Add(subject);
                }
            }

            return (List<Subject>)studentSubjects;
        }

        public CourseSubject GetCourseSubjectByCourseSubjectId(int id)
        {
            CourseSubject courseSubject = db.CourseSubjects.Find(id);

            return courseSubject;
        }

        public void CreateCourseSubjectsFromCourseAndListOfSubjectsAndSaveThemToDB(Course course, List<Subject> subjetcs)
        {            
            foreach(Subject subject in subjetcs)
            {
                CourseSubject courseSubject = new CourseSubject
                {
                    CourseId = course.Id,
                    SubjectId = subject.ID
                };

                db.CourseSubjects.Add(courseSubject);
                db.SaveChanges();

            }
        }

        public List<CourseTeachersStudentsAverageGradesModelView> CreateCourseTeachersStudentsAverageGradesModelView(List<Course> courses)
        {
            teacherService = new TeacherService();
            studentSubjectService = new StudentSubjectService();
            studentService = new StudentService();

            int numberOfTeacherFromACourse;
            int numberOfEnrolledAndRegisteredStudentsInACourse;
            Grade? averageGrade;

            List<CourseTeachersStudentsAverageGradesModelView> viewModel = new List<CourseTeachersStudentsAverageGradesModelView>();

            foreach (Course course in courses)
            {
                numberOfTeacherFromACourse = teacherService.GetNumberOfTeachersPerCourse(course);

                numberOfEnrolledAndRegisteredStudentsInACourse = studentService
                    .GetStudentsRegisteredInACourse(course).Count();

                averageGrade = studentSubjectService.GetStudentAverageGradePerCourse(course);

                CourseTeachersStudentsAverageGradesModelView CourseTeachersStudentsAverageGradesModelView
                    = new CourseTeachersStudentsAverageGradesModelView()
                    {
                        Course = course,
                        numberOfTeachers = numberOfTeacherFromACourse,
                        numberOfStudents = numberOfEnrolledAndRegisteredStudentsInACourse,
                        averageGrade = averageGrade

                    };

                viewModel.Add(CourseTeachersStudentsAverageGradesModelView);
            }

            return viewModel;
        }

        public Course RemoveAllSubjectsFromCourseSubject(Course course)
        {
            IEnumerable<CourseSubject> allCourseSubjectsFromACourse = getAllCourseSubjectsFromCourse(course);

            if(allCourseSubjectsFromACourse.Count() != 0)
            {
                RemoveAllSubjectsFromListOfCourseSubject(allCourseSubjectsFromACourse.ToList());
            }

            return course;
        }

        public List<CourseSubject> CreateCourseSubjectsByIdAndListOfSubjects(int id, List<int> subjectsToAdd)
        {
            List<CourseSubject> newCourseSubjects = new List<CourseSubject>();

            foreach(int subject in subjectsToAdd)
            {
                CourseSubject courseSubject = new CourseSubject()
                {
                    CourseId = id,
                    SubjectId = subject
                };

                newCourseSubjects.Add(courseSubject);

                db.CourseSubjects.Add(courseSubject);
                db.SaveChanges();
            }

            return newCourseSubjects;
        }

        public void DeleteCourseSubject(int id)
        {
            CourseSubject courseSubject = db.CourseSubjects.Find(id);
            db.CourseSubjects.Remove(courseSubject);
            db.SaveChanges();
        }

        public IEnumerable<CourseSubject> getAllCourseSubjectsFromCourse(Course course)
        {
            return db.CourseSubjects
                .Where(cs => cs.CourseId == course.Id);
        }

        public void RemoveAllSubjectsFromListOfCourseSubject(List<CourseSubject> allCourseSubjectsFromACourse)
        {
            foreach (CourseSubject courseSubject in allCourseSubjectsFromACourse)
            {
                db.CourseSubjects.Remove(courseSubject);
            }

            db.SaveChanges();
        }

        public void RemoveCourseSubjetcsByListOfSubjects(int id, List<int> subjectsToRemove)
        {
            List<CourseSubject> courseSubjects = db.CourseSubjects
                .Where(cs => cs.CourseId == id).ToList();

            foreach(CourseSubject courseSubject in courseSubjects)
            {
                foreach(int subjectId in subjectsToRemove)
                {
                    if (courseSubject.SubjectId == subjectId)
                    {
                        db.CourseSubjects.Remove(courseSubject);
                        db.SaveChanges();
                    }

                }
            }
        }

        public CourseSubject GetCourseSubjectByCourseAndSubjectId(int id, int courseId)
        {
            List<CourseSubject> courseSubjects = db.CourseSubjects
                .Where(cs => cs.SubjectId == id).Where(cs => cs.CourseId == courseId).ToList();

            return courseSubjects.ToArray()[0];
        }
    }
}