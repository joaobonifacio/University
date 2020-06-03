using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using University_II.Models;
using University_II.Services;
using University_II.ViewModels;
using System.Data;
using System.Linq;
using System.Web;
using University_II.Migrations;
using System.Threading;

namespace University_II.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TeacherService teacherService;
        private StudentSubjectService studentSubjectService;
        private GradeService gradeService;
        private CourseService courseService;
        private SubjectService subjectService;
        private CourseSubjectService courseSubjectService;
        private StudentService studentService;
        private CourseCreatorSingleton classCreatorSingleton = CourseCreatorSingleton.Instance;


        // GET: Course
        public ActionResult Index()
        {
            courseSubjectService = new CourseSubjectService();
            courseService = new CourseService();

            List <Course> courses = courseService.ListCourses();

            List <CourseTeachersStudentsAverageGradesModelView> viewModel = courseSubjectService
                .CreateCourseTeachersStudentsAverageGradesModelView(courses);

            return View("Index", viewModel);
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            courseService = new CourseService();

            Dictionary<Subject, Teacher> subjectTeacherPairs = courseService
                .getSubjectTeacherPairs((int)id);

            List<TeacherSubjectViewModel> viewModel = courseService
                .CreateTeacherSubjectViewModel(id, subjectTeacherPairs);

            return View(viewModel);
        }

        // GET: Course/Create
        [Authorize]
        public ActionResult Create()
        {
            Course course = new Course();

            return View(course);
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Course course)
        {
            subjectService = new SubjectService();

            if (ModelState.IsValid)
            {
                classCreatorSingleton.ClassName = course.Title;

                List<Subject> potencialSubjects = subjectService.getAllSubjects();

                return View("AddSubjectsToCourse", potencialSubjects);
            }

            return View(course);
        }

        [Authorize]
        public ActionResult ValidateCourseSubject(int chosenSubjectId)
        {
            subjectService = new SubjectService();
            courseSubjectService = new CourseSubjectService();

            if (classCreatorSingleton.allSubjectIds == null || 
                classCreatorSingleton.allSubjectIds.Count() == 0)
            {
                List<Subject> allSubjects = subjectService.getAllSubjects();

                foreach (Subject subject in allSubjects)
                {
                    classCreatorSingleton.allSubjectIds.Add(subject.ID);
                }
            }

            if(classCreatorSingleton.remainingSubjectIds == null 
                || classCreatorSingleton.remainingSubjectIds.Count() == 0)
            {
                List<Subject> subjectsToShow = subjectService.getAllSubjects();

                foreach (Subject subject in subjectsToShow)
                {
                    classCreatorSingleton.remainingSubjectIds.Add(subject.ID);
                }
            }

            classCreatorSingleton.AddSubjectToList(chosenSubjectId);

            List<int> remainingSubjectIds = classCreatorSingleton.getRemainingSubjectIds(chosenSubjectId);

            IEnumerable<Subject> remainingSubjects = subjectService.getSubjectsByListOfId(remainingSubjectIds);

            if(classCreatorSingleton.SubjectIds.Count() == 5)
            {
                Course newCourse = classCreatorSingleton.CreateCourseAndCourseSubjectsAndSaveToDB();

                IEnumerable<Subject> chosenSubjects = classCreatorSingleton.getListOfChosenSubjects();

                courseSubjectService.CreateCourseSubjectsFromCourseAndListOfSubjectsAndSaveThemToDB
                    (newCourse, chosenSubjects.ToList());

                return RedirectToAction("Index");
            }
            
            return View("AddSubjectsToCourse", remainingSubjects);
        }

        [Authorize]
        public ActionResult ChangeCourseSubject(int? id,  int? courseId)
        {
            if(id == 0 || !id.HasValue)
                return RedirectToAction("Index");

            if (courseId == 0 || !courseId.HasValue)
                return RedirectToAction("Index");
            
            courseService = new CourseService();
            subjectService = new SubjectService();
            courseSubjectService = new CourseSubjectService();

            // I have to create a view model which is an enumerable of course and all subjects
            Course course = courseService.GetCourseByCourseId((int)id);
            List<Subject> subjects = subjectService.getAllSubjects();

            IEnumerable<CourseSubject> subjectsToChoose = courseSubjectService
                .CreateCourseSubjectToChooseByCourseIdAndSubjectToDelete((int)courseId, (int)id, subjects);

            // remove old subject from course = delete course subject
            courseSubjectService.DeleteCourseSubjectBySubjectAndCourseIds(id, courseId);

            return View(subjectsToChoose);
        }

        [Authorize]
        public ActionResult AddNewSubjectToCourse(int courseId, int subjectId)
        {
            courseSubjectService = new CourseSubjectService();

            // create new CourseSubject and save it to db
            CourseSubject newCourseSubject = courseSubjectService
                .CreateCourseSubjectByCourseIdAndSubjectId(courseId, subjectId);

            return RedirectToAction("Details", new { id = newCourseSubject.CourseId });
        }


        // GET: Course/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            courseService = new CourseService();

            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            if (User.IsInRole("Dean"))
            {
                Course course = courseService.GetCourseByCourseId((int)id);

                if (course == null)
                {
                    return RedirectToAction("Index");
                }

                return View(course);
            }

            return RedirectToAction("Index");
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Course course)
        {
            if(course == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                courseService = new CourseService();

                courseService.UpdateCourse(course);

                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Course/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            courseService = new CourseService();

            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            if (User.IsInRole("Dean"))
            {
                Course course = courseService.GetCourseByCourseId((int)id);
                
                if (course == null)
                {
                    return RedirectToAction("Index");
                }

                return View(course);
            }

            return RedirectToAction("Index");
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            courseSubjectService = new CourseSubjectService();
            courseService = new CourseService();

            Course course = courseService.GetCourseByCourseId((int)id);

            // remove course subjects
            Course courseWithCourseSubjects = courseSubjectService.RemoveAllSubjectsFromCourseSubject(course);

            // remove course
            List<Course> coursesWithoutCourse = courseService.RemoveCourse(course);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
