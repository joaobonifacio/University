using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using University_II.DTOs;
using University_II.Models;
using University_II.Models.API;
using University_II.Services.API;

namespace University_II.Controllers.API
{
    public class StudentsController : ApiController
    {
        private StudentsService studentsService;
        private StudentToExposeService studentToExposeService;

        // GET /api/students/
        public IHttpActionResult GetEnrolledStudents()
        {
            studentsService = new StudentsService();

            List<StudentToExpose> allStudents = studentsService.GetAllEnrolledStudents();

            if(allStudents.Count() == 0)
            {
                return NotFound();
            }

            return Ok(allStudents);
        }

        // GET /api/students/id
        public IHttpActionResult GetStudent(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            studentsService = new StudentsService();

            Student student = studentsService.GetStudent(id);

            if(student == null)
            {
                return NotFound();
            }

            studentToExposeService = new StudentToExposeService();

            StudentToExpose studentToExpose = studentToExposeService.TrimStudent(student);

            return Ok(studentToExpose);
        } 

        // PUT /api/students/
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateStudent(int id, Student student)
        {
            studentsService = new StudentsService();

            // check if student exists in db.students
            bool studentExistsInDb = studentsService.CheckIfStudentExistsById(id);

            if (!studentExistsInDb)
                return NotFound();

            // if exists update his name in db.students and db.unistudents
            Student updatedStudent = studentsService.UpdateStudent(id, student);

            if(updatedStudent == null)
                return BadRequest();

            studentToExposeService = new StudentToExposeService();

            StudentToExpose studentToExpose = studentToExposeService.TrimStudent(student);

            return Ok(studentToExpose);        
        }

        // DELETE /api/students/
        public IHttpActionResult DeleteStudent(int id)
        {
            studentsService = new StudentsService();

            studentsService.DeleteStudent(id);

            // delete all student subjects

            List<StudentToExpose> allStudents = studentsService.GetAllEnrolledStudents();

            return Ok(allStudents);

        }
    }
}
