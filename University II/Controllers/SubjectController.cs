using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using University_II.Models;
using University_II.Services;
using University_II.ViewModels;

namespace University_II.Controllers
{
    public class SubjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private SubjectService subjectService;
        private TeacherService teacherService;
        private StudentService studentService;
        private StudentSubjectService studentSubjectService;
        private CourseSubjectService courseSubjectService;


        // GET: Subject
        public ActionResult Index()
        {
            teacherService = new TeacherService();
            studentService = new StudentService();
            subjectService = new SubjectService();

            List<Subject> allSubjects = subjectService.getAllSubjects();

            IEnumerable<Teacher> teachers = teacherService
                .GetTeacherNamesFromListOfSubjects(allSubjects);

            List<int> numberOfStudentsEnrolled = studentService.GetNumberOfStudentsEnrolledInASubject(allSubjects);

            List<TeacherSubjectNumberOfStudentsViewModel> viewModel = subjectService
                .CreateTeacherSubjectNumberOfStudentsViewModel
                (allSubjects.ToArray(), teachers.ToArray(), numberOfStudentsEnrolled.ToArray());            

            return View(viewModel);
        }

        // GET: Subject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            subjectService = new SubjectService();
            teacherService = new TeacherService();
            studentSubjectService = new StudentSubjectService();

            Subject subject = subjectService.getSubjectById((int)id);

            Teacher teacher = teacherService.GetTeacherBySubjectId((int)id);

            IEnumerable<StudentSubject> studentSubjects = 
                studentSubjectService.FindStudentSubjectsBySubject(subject);

            if (subject == null || teacher == null || studentSubjects == null)
                return RedirectToAction("Index");
            
            List<TeacherStudentSubjectViewModel> viewModel = subjectService
                .CreateTeacherStudentSubjectViewModel(subject, teacher, studentSubjects);

            return View(viewModel);
        }


        // GET: Subject/Create
        [Authorize]
        public ActionResult Create()
        {
            teacherService = new TeacherService();
            subjectService = new SubjectService();

            Subject subject = new Subject();
            Teacher teacher = new Teacher();
            IEnumerable<Teacher> teachers = teacherService.GetListJustOfTeachers();

            TeacherSubjectViewModel viewModel = subjectService
                .CreateTeacherSubjectViewModel(subject, teacher, teachers);
            
            return View(viewModel);
        }

        // POST: Subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult SaveNewSubject(TeacherSubjectViewModel viewModel)
        {
            subjectService = new SubjectService();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Subject");
            }

            // check if teacher who creates the suubject has already a subject allocated
            if (subjectService.CheckIfTeacherHasSubject(viewModel.Subject.TeacherId)) 
            {
                // find if there are non-allocated teachers and give the subject to the 1st of them
                List<Teacher> nonAllocatedTeachers = subjectService.GetNonAllocatedTeachers();

                // if there's no non-allocated teachers available redirect to...
                if(nonAllocatedTeachers.Count() == 0)
                {
                    return View("NoTeachersAvailable");
                }

                // if there's at leaste one, give the subject to the first of them
                Teacher chosenTeacher = nonAllocatedTeachers.ToArray()[0];

                // create the subject and save it
                subjectService.CreateNewSubjectAndSaveIt(viewModel, chosenTeacher);

                return RedirectToAction("Index");
            }

            subjectService.CreateNewSubjectAndSaveIt(viewModel);

            return RedirectToAction("Index");
        }

        // GET: Subject/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            subjectService = new SubjectService();

            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index", "Subject");
            }

            Subject subject = subjectService.getSubjectById((int)id);

            if (subject == null)
            {
                return RedirectToActionPermanent("Index");
            }

            Teacher teacher = new Teacher();
            IEnumerable<Teacher> teachers = db.Teachers.ToList();

            TeacherSubjectViewModel viewModel = new TeacherSubjectViewModel()
            {
                Subject = subject,
                Teacher = subject.Teacher,
                Teachers = teachers
            };

            return View(viewModel);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(TeacherSubjectViewModel viewModel)
        {
            subjectService = new SubjectService();

            if (ModelState.IsValid)
            {
                subjectService.EditSubject(viewModel);

                return RedirectToAction("Index");
            }

            return null;
        }

        // GET: Subject/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            subjectService = new SubjectService();

            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            Subject subject = subjectService.getSubjectById((int)id);

            if (subject == null)
            {
                return RedirectToAction("Index");
            }

            TeacherSubjectViewModel viewModel = new TeacherSubjectViewModel()
            {
                Subject = subject,
                Teacher = subject.Teacher
            };

            return View(viewModel);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(TeacherSubjectViewModel viewModel)
        {
            subjectService = new SubjectService();

            subjectService.DeleteSubjectByTeacherSubjectViewModel(viewModel);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult EditCredits(int? id, int? courseId)
        {
            courseSubjectService = new CourseSubjectService();

            if (id == 0)
            {
                return RedirectToAction("Index", "Subject");
            }

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Subject");
            }

            CourseSubject courseSubject = courseSubjectService
                .GetCourseSubjectByCourseAndSubjectId((int)id, (int)courseId);            
            
            if (courseSubject == null)
            {
                return RedirectToActionPermanent("Index");
            }

            return View(courseSubject);
        }

        [Authorize]
        public ActionResult SaveCredits(Subject subject)
        {
            subjectService = new SubjectService();

            if (subject == null)
            {
                return RedirectToAction("Index");
            }

            if (subject.ID == 0)
            {
                return RedirectToAction("Index");
            }

            if(!subjectService.ChangeCredits(subject.ID, subject.Credits))
            {
                return RedirectToAction("EditCredits", new { id = subject.ID });
            }

            return RedirectToAction("Index", "Course");
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
