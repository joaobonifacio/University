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

namespace University_II.Controllers
{
    public class CourseSubjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CourseSubjectService courseSubjectService;

        // GET: CourseSubject
        public ActionResult Index()
        {
            courseSubjectService = new CourseSubjectService();

            var courseSubjects = courseSubjectService.GetAllCourseSubjects();

            return View(courseSubjects);
        }

        // GET: CourseSubject/Details/5
        public ActionResult Details(int? id)
        {
            courseSubjectService = new CourseSubjectService();

            if (id == null || !id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CourseSubject courseSubject = courseSubjectService.GetCourseSubjectByCourseSubjectId((int)id);

            if (courseSubject == null)
            {
                return RedirectToAction("Index");
            }
            return View(courseSubject);
        }

        // GET: CourseSubject/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
            ViewBag.SubjectId = new SelectList(db.Subjects, "ID", "Title");
            return View();
        }

        // POST: CourseSubject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseId,SubjectId")] CourseSubject courseSubject)
        {
            if (ModelState.IsValid)
            {
                db.CourseSubjects.Add(courseSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", courseSubject.CourseId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "ID", "Title", courseSubject.SubjectId);
            return View(courseSubject);
        }

        // GET: CourseSubject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSubject courseSubject = db.CourseSubjects.Find(id);
            if (courseSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", courseSubject.CourseId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "ID", "Title", courseSubject.SubjectId);
            return View(courseSubject);
        }

        // POST: CourseSubject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,SubjectId")] CourseSubject courseSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", courseSubject.CourseId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "ID", "Title", courseSubject.SubjectId);
            return View(courseSubject);
        }

        // GET: CourseSubject/Delete/5
        public ActionResult Delete(int? id)
        {
            courseSubjectService = new CourseSubjectService();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CourseSubject courseSubject = courseSubjectService.GetCourseSubjectByCourseSubjectId((int)id);

            if (courseSubject == null)
            {
                return HttpNotFound();
            }
            return View(courseSubject);
        }

        // POST: CourseSubject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            courseSubjectService = new CourseSubjectService();

            courseSubjectService.DeleteCourseSubject(id);

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
