using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using University_II.DTOs;
using University_II.Models;
using University_II.Services.API;
using AutoMapper;


namespace University_II.Controllers.API
{
    public class NonAllocatedTeachersController : ApiController
    {
        private TeachersService teachersService;

        // GET non-allocated teachers
        public IHttpActionResult GetNonAllocatedTeachers()
        {
            teachersService = new TeachersService();

            List<Teacher> teachers = teachersService.GetNonAllocatedTeachers();

            if(teachers.Count() == 0)
            {
                return NotFound();
            }

            return Ok(teachers.Select(Mapper.Map<Teacher, TeacherDTO>));
        }
    }
}
