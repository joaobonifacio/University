using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using University_II.Models;
using University_II.Models.API;
using University_II.Services.API;

namespace University_II.Controllers.API
{
    public class StudentSubjectsController : ApiController
    {
        private StudentSubjectsService studentSubjectsService;
        private StudentSubjectToExposeService studentSubjectToExposeService;

        // GET /api/studentssubjects/
        public IHttpActionResult GetStudentSubjects()
        {
            studentSubjectsService = new StudentSubjectsService();
            studentSubjectToExposeService = new StudentSubjectToExposeService();

            List<StudentSubject> studentSubjects = studentSubjectsService.GetAllStudentSubjects();

            if (studentSubjects.Count() == 0)
                return NotFound();

            // turn student subjects into studentsubjects to expose
            List<StudentSubjectToExpose> studentSubjectsToExpose = studentSubjectToExposeService
                .TrimStudentSubjects(studentSubjects);

            return Ok(studentSubjectsToExpose);

        }

        // GET /api/students/id
        public IHttpActionResult GetStudentSubject(int id)
        {
            studentSubjectsService = new StudentSubjectsService();
            studentSubjectToExposeService = new StudentSubjectToExposeService();

            StudentSubject studentSubject = studentSubjectsService.GetStudentSubject(id);

            if (studentSubject == null)
                return NotFound();

            // turn student subject into studentsubject to expose
            StudentSubjectToExpose studentSubjectToExpose = studentSubjectToExposeService
                .TrimStudentSubject(studentSubject);

            return Ok(studentSubjectToExpose);
        }

        // PUT /api/studentsubjects/
        [HttpPut]
        public IHttpActionResult UpdateStudentSubject(int id, StudentSubject studentSubject)
        {
            studentSubjectsService = new StudentSubjectsService();
            studentSubjectToExposeService = new StudentSubjectToExposeService();

            StudentSubject theStudentSubject = new StudentSubject();

            theStudentSubject.Grade = studentSubject.Grade;

            StudentSubject newStudentSubject = studentSubjectsService
            .UpdateStudentService(id, studentSubject);

            if (newStudentSubject == null)
                return BadRequest();

            StudentSubjectToExpose newStudentSubjectToExpose = studentSubjectToExposeService
            .TrimStudentSubject(newStudentSubject);

            return Ok(newStudentSubjectToExpose);

        }
    }
}
