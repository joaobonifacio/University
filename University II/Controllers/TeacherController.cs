using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using University_II.Models;
using University_II.Services;
using University_II.ViewModels;

namespace University_II.Controllers
{
    public class TeacherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TeacherService teacherService;
        private StudentSubjectService studentSubjectService;
        private SubjectService subjectService;
        private StudentService studentService;
        private StaffService staffService;
        private HireTeacherSingleton hireTeacherSingleton = HireTeacherSingleton.Instance;



        // GET: Teacher
        public ActionResult Index()
        {
            teacherService = new TeacherService();

            List<Teacher> teachers = new List<Teacher>();
            List<TeacherSubjectPairsViewModel> teacherSubjectPairs = 
                new List<TeacherSubjectPairsViewModel>();

            teacherSubjectPairs = teacherService.GetListOfTeachers();

           return View("Index", teacherSubjectPairs);
        }

        public ActionResult TeacherStudentsAndGrades(int? teacherId)
        {
            if(teacherId == 0 || !teacherId.HasValue)
            {
                return RedirectToAction("Index");
            }

            teacherService = new TeacherService();
            studentService = new StudentService();

            Teacher teacher = teacherService.getCorrespondingTeacher((int)teacherId);

            if (teacher  == null)
                return RedirectToAction("Index");

            IEnumerable<StudentSubject> teacherStudentSubjects = teacherService
                .GetTeacherStudentSubjectsByTeacherId((int)teacherId);

            if (teacherStudentSubjects.Count() == 0)
                return RedirectToAction("Index");

            IEnumerable<Student> teachersStudents = studentService
                .GetStudentsByStudentSubjects(teacherStudentSubjects.ToList());

            if (teachersStudents.Count() == 0)
                return RedirectToAction("Index");

            IEnumerable<Subject> teachersSubjects = teacherService
                .GetTeachersSubjectsFromTeachersStudentSubjectList(teacherStudentSubjects);

            if (teachersSubjects.Count() == 0)
                return RedirectToAction("Index");

            List<TeacherStudentsAndGradesViewModel> viewModel = teacherService
                .CreateTeacherStudentsAndGradesViewModel(teacher, teacherStudentSubjects, teachersStudents, 
                teachersSubjects);

            return View(viewModel);
        }

        [Authorize]
        public ActionResult EditGrade(int? studentSubjectId)
        {
            if(studentSubjectId == 0 || !studentSubjectId.HasValue)
            {
                return RedirectToAction("Index");
            }

            StudentSubject studentSubject = studentSubjectService
                .GetStudentSubjectByStudentSubjectId((int)studentSubjectId);
            
            return View("EditGrade", studentSubject);

        }

        [Authorize]
        public ActionResult GradeStudents(int subjectID)
        {
            studentSubjectService = new StudentSubjectService();

            List<StudentSubject> studentSubjects = studentSubjectService
                .GetAllStudentSubjectsRelatedToASubject(subjectID);

            return View("AlternativeGradeStudents", studentSubjects);
        }

        [Authorize]
        public ActionResult SaveGrade(StudentSubject studentSubject)
        {
            if(studentSubject.Grade == null)
            {
                return RedirectToAction("EditGrade", 
                    new { studentSubjectId = studentSubject.StudentSubjectID });
            }

            studentSubjectService = new StudentSubjectService();
            teacherService = new TeacherService();

            studentSubjectService.SaveGrade(studentSubject);

            Teacher teacher = teacherService
                .GetTeacherByStudentSubjectId(studentSubject.StudentSubjectID);

            return RedirectToAction("MyStudents", "Teacher", new { email = teacher.Email });
        }

        [Authorize]
        public ActionResult MySubjects(string email)
        {
            if(email == null)
            {
                return RedirectToAction("Index");
            }

            subjectService = new SubjectService();
            teacherService = new TeacherService();

           int teacherId = teacherService.GetTeacherIdFromEmail(email);

            Teacher teacher = teacherService.GetTeacherById(teacherId);

            IEnumerable<Subject> teacherSubjects = subjectService.getSubjectsByTeacherId(teacherId);

            List<TeacherSubjectNumberOfStudentsViewModel> viewModel = teacherService
                .CreateTeacherSubjectNumberOfStudentsViewModel(teacher, teacherSubjects);

            return View(viewModel);
        }

        [Authorize]
        public ActionResult MyStudents(string email)
        {
            if(email == null)
            {
                return RedirectToAction("Index");
            }

            studentSubjectService = new StudentSubjectService();
            teacherService = new TeacherService();
            subjectService = new SubjectService();

            int teacherId = teacherService.GetTeacherIdFromEmail(email);

            List<Subject> subjectsTaught = subjectService.getSubjectsByTeacherId(teacherId);

            List<StudentSubject> studentSubjects = studentSubjectService
                .GetStudentSubjectListFromALIstOfSubjects(subjectsTaught);

            return View(studentSubjects);
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int? teacherId)
        {
            if(teacherId == null || !teacherId.HasValue)
            {
                return RedirectToAction("Index");
            }

            teacherService = new TeacherService();

            Teacher teacher = teacherService.GetTeacherById((int)teacherId);

            if (teacher == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(teacher);
        }

        [Authorize]
        public ActionResult ChangeTeacher(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            teacherService = new TeacherService();
            subjectService = new SubjectService();

            // assign the value of teacher to be replaced to HireTeacherSingleton
            hireTeacherSingleton.TeacherToBeReplaced =  teacherService.GetTeacherById((int)id);

            // assing the subject where teacher will be replaced
            hireTeacherSingleton.SubjectWhereTeacherWillBeReplaced = subjectService
                .getTeacherSubjectByTeacherId((int)id);

            // create view model for dropdown list choice between allocated and non-allocated teachers
            teacherService = new TeacherService();
            TeachersToReplaceViewModel teachers = null;

            List<Teacher> nonAllocatedTeachers = teacherService.GetNonAllocatedTeachers();

            teachers = new TeachersToReplaceViewModel()
            {
                Id = 0,
                teachers = nonAllocatedTeachers
            };


            return View("TypeOfReplacement", teachers);
        }

        [Authorize]
        public ActionResult SaveReplacement(TeachersToReplaceViewModel model)
        {
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            subjectService = new SubjectService();
            teacherService = new TeacherService();

            Teacher newTeacher = teacherService.GetTeacherById(model.Id);
            
            Subject subjectWithNewTeacher = hireTeacherSingleton.SubjectWhereTeacherWillBeReplaced;
            
            // assign new teacher to subject and save changes
            subjectService.ChangeSubjectTeacher(subjectWithNewTeacher, newTeacher);
            
            return RedirectToAction("Index");
        }

        // GET: Teacher/Create
        [Authorize]
        public ActionResult Create()
        {
            Teacher teacher = new Teacher();

            return View(teacher);
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            if(teacher != null)
            {
                teacherService = new TeacherService();
                teacherService.AddAndSaveNewTeacher(teacher);
                
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teacher/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index)");
            }

            teacherService = new TeacherService();

            Teacher teacher = teacherService.GetTeacherById((int)id);

            if (teacher == null)
            {
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Teacher teacher)
        {
            if(teacher == null)
            {
                return RedirectToAction("Index)");
            }

            if (!ModelState.IsValid)
            {
                return View(teacher);
            }
            
            teacherService = new TeacherService();
            teacherService.EditTeacher(teacher, teacher.Id);
            
            return RedirectToAction("Index");
        }

        // GET: Teacher/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            teacherService = new TeacherService();

            Teacher teacher = teacherService.GetTeacherById((int)id);

            if (teacher == null)
            {
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int? id)
        {
            if(id == 0 || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            teacherService = new TeacherService();
            subjectService = new SubjectService();
            staffService = new StaffService();

            // pass subjects to singleton
            List<Subject> subjects = subjectService.getSubjectsByTeacherId((int)id);
            hireTeacherSingleton.Subjects = subjects;

            // remove teacher from staff
            staffService.DeleteTeacherFromStaffById((int)id);

            //delete the teacher
            teacherService.DeleteTeacherById((int)id);

            // if teacher had a subject allocated substitue him, if not don't
            if(subjects.Count == 0)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("HireNewTeacher");
        }

        [Authorize]
        public ActionResult HireNewTeacher()
        {
            Teacher teacher = new Teacher();

            HireNewTeacherViewModel model = new HireNewTeacherViewModel()
            {
                Teacher = teacher,
                Subjects = hireTeacherSingleton.getSubjects()
            };

            return View(model);
        }

        [Authorize]
        public ActionResult SaveNewTeacher(Teacher teacher)
        {
            teacherService = new TeacherService();
            subjectService = new SubjectService();
            staffService = new StaffService();

            // adds it to the teacher's list and the staff's list
            teacherService.AddAndSaveNewTeacher(teacher);

            subjectService.ChangeTeacherInAListaOfSubjects(hireTeacherSingleton.getSubjects(), teacher);

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
