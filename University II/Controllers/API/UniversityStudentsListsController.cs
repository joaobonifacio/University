using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using University_II.DTOs;
using University_II.Models;
using University_II.Services.API;

namespace University_II.Controllers.API
{
    public class UniversityStudentsListsController : ApiController
    {
        private UniversityStudentsListsService universityStudentsListsService;

        // GET /api/universitystudenteslistss/
        public IHttpActionResult GetUniversityStudentsLists()
        {
            universityStudentsListsService = new UniversityStudentsListsService();

            return Ok(universityStudentsListsService
                .GetAllUniversityStudentsLists().Select(Mapper.Map<UniversityStudentsList, 
                UniversityStudentListDTO>));
        }

        // GET /api/universitystudenteslistss/id
        public IHttpActionResult GetUniversityStudentsList(int id)
        {
            if(id == 0)
                BadRequest();

            universityStudentsListsService = new UniversityStudentsListsService();

            UniversityStudentsList uniStudent = universityStudentsListsService
                .GetUniversityStudentsList(id);

            if(uniStudent == null)
                NotFound();

            UniversityStudentListDTO uniStudentDTO = Mapper.Map<UniversityStudentsList,
            UniversityStudentListDTO>(uniStudent);

            return Ok(uniStudentDTO);
        }

        // POST /api/universitystudenteslistss
        [HttpPost]
        public IHttpActionResult CreateUniversityStudent(UniversityStudentListDTO uniStudentDTO)
        {
            universityStudentsListsService = new UniversityStudentsListsService();

            UniversityStudentsList theUniStudent = Mapper.Map<UniversityStudentListDTO,
                UniversityStudentsList>(uniStudentDTO);

            UniversityStudentsList universityStudent = universityStudentsListsService
                .CreateUniversityStudent(theUniStudent);

            if (universityStudent == null)
            {
                return BadRequest();
            }

            UniversityStudentListDTO theUniversityStudentDTO = Mapper.Map<UniversityStudentsList,
                UniversityStudentListDTO>(universityStudent);

            return Created(new Uri(Request.RequestUri + "/" + theUniversityStudentDTO.Id),
                theUniversityStudentDTO);
        }

        // PUT api/universitystudenteslistss/id
        [HttpPut]
        public IHttpActionResult UpdateUniversityStudent(int id, UniversityStudentListDTO uniStudentDTO)
        {
            universityStudentsListsService = new UniversityStudentsListsService();
            UniversityStudentsList updatedStudent = new UniversityStudentsList();

            //check if uniStudent exists in University Students List db
            bool universityStudentExistsInDb = universityStudentsListsService
                .CheckIfStudentExistsInUniversityListDb(id);

            if (!universityStudentExistsInDb)
                return NotFound();

            // get previsous version of uniStudent
            UniversityStudentsList previousUniStudent = universityStudentsListsService
                .GetUniversityStudentsList(id);

            // check if uniStudent exists in db.Students
           Student student = universityStudentsListsService
                .CheckIfUniversityStudentExistsInStudentsDB(previousUniStudent);

            if (student == null)
            {
                UniversityStudentsList aStudent = Mapper.Map<UniversityStudentListDTO,
                    UniversityStudentsList>(uniStudentDTO);

                updatedStudent = universityStudentsListsService.UpdateUniversityStudent(id, aStudent);

                UniversityStudentListDTO updatedUniversityStudentDTO = Mapper
                    .Map<UniversityStudentsList, UniversityStudentListDTO>(updatedStudent);

                return Ok(updatedUniversityStudentDTO);
            }

            UniversityStudentsList anotherStudent = Mapper.Map<UniversityStudentListDTO,
                    UniversityStudentsList>(uniStudentDTO);

            updatedStudent = universityStudentsListsService.UpdateUniversityStudent(id, anotherStudent);
            universityStudentsListsService.UpdateStudentByUniStudent(updatedStudent, student);

            UniversityStudentListDTO anotherUpdatedUniversityStudentDTO = Mapper
                    .Map<UniversityStudentsList, UniversityStudentListDTO>(updatedStudent);

            return Ok(anotherUpdatedUniversityStudentDTO);
        }
    }
}
