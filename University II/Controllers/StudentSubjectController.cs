using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University_II.Models;
using University_II.Services;

namespace University_II.Controllers
{
    public class StudentSubjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private StudentSubjectService studentSubjectService = new StudentSubjectService();

        // GET: StudentSubject
        [Authorize]
        public ActionResult Index()
        {
            var studentSubjects = db.StudentSubjects.Include(s => s.Student).Include(s => s.Subject);
            return View(studentSubjects.ToList());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(StudentSubject studentSubject)
        {
            studentSubjectService = new StudentSubjectService();

            var studentSubjectInDB = studentSubjectService.SaveStudentSubjectGrade(studentSubject);

            return RedirectToAction("GradeStudents", "Teacher", 
                new { SubjectTitle = studentSubjectInDB.Subject.Title});
        }

        // GET: StudentSubject/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            studentSubjectService = new StudentSubjectService();

            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            StudentSubject studentSubject = studentSubjectService
                .GetStudentSubjectByStudentSubjectId((int)id);
            
            if (studentSubject == null)
            {
                return RedirectToAction("Index");
            }
            return View(studentSubject);
        }

        // GET: StudentSubject/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name");
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Title");
            return View();
        }

        // POST: StudentSubject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "StudentSubjectID,SubjectID,StudentID,Grade")] StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                db.StudentSubjects.Add(studentSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", studentSubject.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Title", studentSubject.SubjectID);
            return View(studentSubject);
        }

        // GET: StudentSubject/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", studentSubject.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Title", studentSubject.SubjectID);
            return View(studentSubject);
        }

        // POST: StudentSubject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "StudentSubjectID,SubjectID,StudentID,Grade")] StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", studentSubject.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Title", studentSubject.SubjectID);
            return View(studentSubject);
        }

        // GET: StudentSubject/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            studentSubjectService = new StudentSubjectService();

            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            StudentSubject studentSubject = studentSubjectService
                .GetStudentSubjectByStudentSubjectId((int)id);
            
            if (studentSubject == null)
            {
                return RedirectToAction("Index");
            }
            return View(studentSubject);
        }

        // POST: StudentSubject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            studentSubjectService = new StudentSubjectService();

            studentSubjectService.DeleteStudentSubjectById((int)id);

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
