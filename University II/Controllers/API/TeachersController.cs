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
using University_II.Services;
using University_II.Services.API;

namespace University_II.Controllers.API
{
    public class TeachersController : ApiController
    {
        TeachersService teachersService;

        // GET /api/teachers/
        public IHttpActionResult GetTeachers()
        {
            teachersService = new TeachersService();

            if(teachersService.GetListJustOfTeachers().Count() == 0)
            {
                return NotFound();
            }

            return Ok(teachersService.GetListJustOfTeachers().Select(Mapper.Map<Teacher, TeacherDTO>));

        }

        // GET /api/teachers/id
        public IHttpActionResult GetTeacher(int id)
        {
            teachersService = new TeachersService();

            if(id == 0)
            {
                return BadRequest();
            }

            Teacher teacher = teachersService.GetTeacherByID(id);

            if(teacher == null)
            {
                return NotFound();
            }

            TeacherDTO teacherDTO = Mapper.Map<Teacher, TeacherDTO>(teacher);

            return Ok(teacherDTO);
        }
        
        // POST /api/teachers/
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult CreateTeacher(TeacherDTO teacherDTO)
        {
            teachersService = new TeachersService();

            Teacher teacher = Mapper.Map<TeacherDTO, Teacher>(teacherDTO);

            // check if teacher already exists
            bool teacherExists = teachersService.CheckIfTeacherAlreadyExists(teacher);

            if (teacherExists)
                return BadRequest();

            Teacher newTeacher = teachersService.CreateNewTeacher(teacher);

            if (teacher == null)
                return BadRequest();

            // add teacher to staff
            teachersService.AddTeacherToStaff(teacher);

            TeacherDTO theTeacherDTO = Mapper.Map<Teacher, TeacherDTO>(teacher);


            return Created(new Uri(Request.RequestUri + "/" + theTeacherDTO.Id), theTeacherDTO);

        }


        // PUT /api/teachers/id
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateTeacher(int id, TeacherDTO teacherDTO)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            Teacher teacher = Mapper.Map<TeacherDTO, Teacher>(teacherDTO);

            if (teacher == null)
            {
                return BadRequest();
            }

            teachersService = new TeachersService();
            Teacher theTeacher = teachersService.UpdateTeacher(id, teacher);

            TeacherDTO theTeacherDTO = Mapper.Map<Teacher, TeacherDTO>(theTeacher);

            return Ok(theTeacherDTO);
        }

        // DELETE /api/teachers/id
        public IHttpActionResult DeleteTeacher(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            teachersService = new TeachersService();

            bool teacherHasSubject = false;

            teacherHasSubject = teachersService.CheckIfTeacherHasSubject(id);

            if(!teacherHasSubject)
            {
                teachersService.DeleteTeacherById(id);

                return Ok(teachersService.GetListJustOfTeachers());
            }

            bool teacherHasAReplacementAvailable = teachersService
                .GetNonAllocatedTeachers().Count() > 0 ? true : false;

            if (!teacherHasAReplacementAvailable)
            {
                return BadRequest();
            }

            // deletes from teachers table and staff table
            teachersService.DeleteTeacherAndSubstituteHim(id);

            return Ok(teachersService.GetListJustOfTeachers().Select(Mapper.Map<Teacher, TeacherDTO>));


        }
    }
}
