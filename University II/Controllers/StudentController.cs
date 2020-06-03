using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using University_II.Models;
using University_II.Services;
using University_II.ViewModels;

namespace University_II.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private StudentSubjectService studentSubjectService;
        private CourseService courseService;
        private StudentService studentService;
        private GradeService gradeService;
        private SubjectService subjectService;
        private UniversityStudentsListService uniService;

        // GET: Student
        public ActionResult Index()
        {
            studentSubjectService = new StudentSubjectService();
            courseService = new CourseService();
            studentService = new StudentService();
            subjectService = new SubjectService();

            List<StudentSubject> studentSubjects = studentSubjectService.GetAllStudentSubjects();
            
            IEnumerable<Subject> subjects = subjectService.GetSubjectsFromListOfStudentSubjects(studentSubjects);

            IEnumerable<Course> courses = courseService.GetCoursesFromListOfStudentSubjects(studentSubjects);

            IEnumerable<Student> students = studentService.GetStudentsByStudentSubjects(studentSubjects);

            List<CourseStudentSubjectViewModel> viewModel = studentService
                .CreateCourseStudentSubjectViewModel(subjects, courses, students, studentSubjects);

            return View(viewModel);
        }

        [Authorize]
        public ActionResult MySubjects(string email)
        {
            if(email == null)
            {
                return RedirectToAction("Index");
            }

            studentSubjectService = new StudentSubjectService();
            studentService = new StudentService();
            courseService = new CourseService();

            Student theStudent = studentService.GetStudentByName(email);

            if(theStudent == null)
            {
                return RedirectToAction("Index");
            }

            List<StudentSubject> studentSubjects = studentSubjectService
                .GetStudentSubjectsByStudentId(theStudent.ID);

            Course studentCourse = courseService.GetStudentCourseByStudent(theStudent);

            List<StudentMySubjectsViewModel> viewModel = studentService
                .CreateStudentMySubjectsViewModel(studentCourse, theStudent, studentSubjects);

            return View(viewModel);
        }

        public ActionResult Grades(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            studentSubjectService = new StudentSubjectService();

            List<StudentSubject> studentSubjects = studentSubjectService.GetStudentSubjectsByStudentId((int)id);

            if(studentSubjects.Count() == 0)
            {
                return RedirectToAction("Index");
            }

            return View(studentSubjects);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index", "Student");
            }

            studentSubjectService = new StudentSubjectService();
            gradeService = new GradeService();
            studentService = new StudentService();

            Student student = studentService.GetStudentByID((int)id);

            if (student == null)
            {
                return RedirectToAction("Index");
            }

            ICollection<StudentSubject> studentSubjects = studentSubjectService
                .GetStudentSubjectsByStudentId((int)id);

            Grade? averageGrade = studentSubjectService.GetStudentAverageGrade((int)id);

            Course course = studentService.GetStudentCourse((int)id);

            StudentAverageGradeViewModel viewModel = studentService
                .CreateStudentAverageGradeViewModel(student, course, averageGrade);

            return View(viewModel);
        }

        
        // GET: Student/Create
        [Authorize]
        public ActionResult CreateUniversityStudent()
        {
            courseService = new CourseService();

            UniversityStudentsList newStudent = new UniversityStudentsList();

            IEnumerable<Course> courses = courseService.ListCourses();

            UniversityStudentCreationViewModel viewModel = new UniversityStudentCreationViewModel()
            {
                Student = newStudent,
                Courses = courses
            };

            return View("CreateUniversityStudent", viewModel);
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateUniversityStudent(UniversityStudentsList student)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            if (student == null)
            {
                return RedirectToAction("Index");
            }
            
            uniService = new UniversityStudentsListService();
            
            // put student on the university students list
            uniService.AddNewStudentToUniversityStudentsLists(student);
            
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            studentService = new StudentService();

            Student student = studentService.GetStudentByID((int)id);

            if (student == null)
            {
                return RedirectToAction("Index");
            }

            EditStudentViewModel viewModel = new EditStudentViewModel()
            {
                Email = student.Email,
                Name = student.Name,
                StudentId = student.ID
            };

            return View(viewModel);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(EditStudentViewModel editedStudent)
        {
           
                if(editedStudent.Email == null || editedStudent.Name == null 
                    || editedStudent.StudentId == 0)
                {
                    return RedirectToAction("Index");
                }
                
                studentService = new StudentService();
                
                studentService.UpdateStudent(editedStudent);
                
                return RedirectToAction("Index");
        }

        // GET: Student/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            studentService = new StudentService();

            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            Student student = studentService.GetStudentByID((int)id);

            if (student == null)
            {
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            uniService = new UniversityStudentsListService();
            studentService = new StudentService();
            studentSubjectService = new StudentSubjectService();

            if (id != 0)
            {
                // first remove student's studentsubjects
                studentSubjectService.RemoveStudentsStudentSubjectsByStudentId(id);

                // then set is enrolled to false in university students list
                uniService.SetIsEnrolledToFalseByStudentId(id);

                // and finally delete student from db.students
                studentService.DeleteStudentByStudentId(id);

                return RedirectToAction("Index");
            }

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
