using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using University_II.Models;
using University_II.Services;
using System.Data.Entity;
using University_II.Models.API;
using University_II.Services.API;
using System.Data;
using University_II.DTOs;
using AutoMapper;

namespace University_II.Controllers
{
    public class SubjectsController : ApiController
    {
        SubjectToExposeService subjectToExposeService;
        SubjectsService subjectsService;
        TeachersService teachersService;

        // GET /api/subjects/
        public IHttpActionResult GetSubjects()
        {
            subjectsService = new SubjectsService();

            List<SubjectToExpose> subjectsToExpose = subjectsService.GetSubjects();

            if(subjectsToExpose.Count() == 0)
            {
                return NotFound();
            }

            return Ok(subjectsToExpose);
        }

        // GET /api/subject/id
        public IHttpActionResult GetSubject(int id)
        {
            subjectsService = new SubjectsService();
            subjectToExposeService = new SubjectToExposeService();

            if (id == 0)
            {
                return NotFound();
            }
            
            Subject subject = subjectsService.GetSubjectById(id);

            if(subject == null)
            {
                return NotFound();
            }

            SubjectToExpose subjectToExpose = subjectToExposeService.TrimSubject(subject);

            return Ok(subjectToExpose);
        }

        // POST /api/subjects
        public IHttpActionResult CreateSubject(SubjectDTO subjectDTO)
        {
            if(subjectDTO == null)
            {
                return BadRequest();
            }

            subjectsService = new SubjectsService();
            teachersService = new TeachersService();

            Subject subject = Mapper.Map<SubjectDTO, Subject>(subjectDTO);

            // check if the subject already exists
            Subject newSubject = subjectsService.CheckIfSubjectExistsByName(subject);

            if (newSubject.ID != 0)
                return BadRequest();

            // if the subject teacher already has a subject it's a bad request
            bool teacherHasASubject = false;
            teacherHasASubject = teachersService.CheckIfTeacherHasSubject(subject.TeacherId);

            if (teacherHasASubject)
            {
                return BadRequest();
            }

            // you can only create a subject if there's a teacher available to teach it
            if (teachersService.GetNonAllocatedTeachers().Count() <= 0) 
            {
                return BadRequest();
            };

            // will give the subject to a non-allocated teacher
            Subject theSubject = subjectsService.CreateSubject(subject);

            if(theSubject == null)
            {
                return NotFound();
            }

            SubjectDTO theSubjectDTO = Mapper.Map<Subject, SubjectDTO>(theSubject);

            return Created(new Uri(Request.RequestUri + "/" + theSubjectDTO.ID), theSubjectDTO);
        }

        // PUT /api/subject/id
        [HttpPut]
        public IHttpActionResult UpdateSubject(int id, SubjectDTO subjectDTO)
        {
            subjectsService = new SubjectsService();
            subjectToExposeService = new SubjectToExposeService();

            if (id == 0)
            {
                return NotFound();
            }

            if(subjectDTO == null)
            {
                return BadRequest();
            }

            Subject subject = Mapper.Map<SubjectDTO, Subject>(subjectDTO);
            Subject theSubject = new Subject();

            theSubject = subjectsService.UpdateSubject(id, subject);

            if (theSubject  == null) 
            {
                return BadRequest();
            }

            //SubjectToExpose subjectToExpose = subjectToExposeService.TrimSubject(subject);

            SubjectDTO theSubjectDTO = Mapper.Map<Subject, SubjectDTO>(theSubject);

            return Ok(theSubjectDTO);
        }

        // DELETE /api/subjects/id
        public IHttpActionResult DeleteSubject(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            subjectsService = new SubjectsService();

            if (!subjectsService.CheckIfSubjectExists(id))
            {
                return NotFound();
            }
            
            subjectsService.DeleteSubject(id);

            List<SubjectToExpose> subjectsToExpose = subjectsService.GetSubjects();

            return Ok(subjectsToExpose);
        }
    }
}
