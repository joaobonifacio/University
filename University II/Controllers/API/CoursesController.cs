using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using University_II.DTOs;
using University_II.Models;
using University_II.Models.API;
using University_II.Services.API;

namespace University_II.Controllers.API
{
    public class CoursesController : ApiController
    {
        private CoursesService coursesService;
        private SubjectsService subjectsService;
        private TeachersService teachersService;
        private SubjectToExposeService subjectToExposeService;
        private CourseToExposeService courseToExposeService;


        // GET /api/courses
        public IHttpActionResult GetCourses()
        {
            coursesService = new CoursesService();

            return Ok(coursesService.ListCourses().Select(Mapper.Map<Course, CourseDTO>));
        }

        // GET /api/courses/id
        public IHttpActionResult GetCourse(int id)
        {
            coursesService = new CoursesService();

            Course course = new Course();

            course = coursesService.GetCourseByCourseId(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Course, CourseDTO>(course));
        }

        // POST /api/courses
        [HttpPost]
        public IHttpActionResult CreateCourse(CourseCreator courseCreator)
        {
            coursesService = new CoursesService();
            subjectsService = new SubjectsService();
            teachersService = new TeachersService();
            subjectToExposeService = new SubjectToExposeService();
            courseToExposeService = new CourseToExposeService();

            if (courseCreator == null)
            {
                return BadRequest();
            }

            bool exists = false;
            exists = coursesService.CheckIfCourseAlreadyExists(courseCreator.Title);

            if (exists)
            {
                return BadRequest();
            }

            List<CourseSubject> courseSubjects = coursesService.CreateCourse(courseCreator);

            List<SubjectToExpose> subjectsToExpose = new List<SubjectToExpose>();

            foreach(CourseSubject courseSubject in courseSubjects)
            {
                Subject subject = new Subject();
                subject = subjectsService.Find(courseSubject.SubjectId);

                if(subject == null)
                {
                    return BadRequest();
                }

                if(subject.ID == 0)
                {
                    return BadRequest();
                }

                Teacher teacher = new Teacher();

                teacher = teachersService.GetTeacherNameByID(subject.TeacherId);

                if(teacher == null)
                {
                    return BadRequest();
                }

                SubjectToExpose subjectToExpose = subjectToExposeService.TrimSubject(subject);

                subjectsToExpose.Add(subjectToExpose);
            }

            CourseToExpose course = courseToExposeService
                .TrimSubject(courseSubjects, courseCreator, subjectsToExpose);

            return Created(new Uri(Request.RequestUri + "/" + course.Id), course);
        }

        // PUT /api/courses/id
        [HttpPut]
        public IHttpActionResult UpdateCourse(int id, CourseCreator courseCreator)
        {
            coursesService = new CoursesService();
            courseToExposeService = new CourseToExposeService();

            if (courseCreator == null)
            {
                return NotFound();
            }

            if(courseCreator.SubjectIds.Count() > 5)
                return BadRequest();
            

            // check if course exists; if it doesn't, send NotFound()
           Course courseToUpdate = coursesService.GetCourseByCourseId(id);
            
            if (courseToUpdate == null)
                return BadRequest();

            // check if title changed and if so update it
            courseToUpdate = coursesService.UpdateTitle(id, courseCreator.Title);

            if (courseToUpdate == null)
                return BadRequest();

            // list of subjects of course before update
            List<int> oldSubjectList = coursesService.GetCourseSubjectsIds(courseToUpdate);

            // new list of subjects
            List<int> newSubjectList = courseCreator.SubjectIds;

            // compare list of subjects 
            List<int> subjectsToRemove = new List<int>();
            List<int> subjectsToAdd = new List<int>();

            foreach (int oldSubject in oldSubjectList)
            {
                if (!newSubjectList.Contains(oldSubject))
                {
                    subjectsToRemove.Add(oldSubject);
                }
            }

            foreach (int newSubject in newSubjectList)
            {
                if (!oldSubjectList.Contains(newSubject))
                {
                    subjectsToAdd.Add(newSubject);
                }
            }

            // create courseSubjects to new subjects
            List<CourseSubject> newCourseSubjects = coursesService
                .CreateCourseSubjectsByIdAndListOfSubjects(courseToUpdate.Id, subjectsToAdd);

            // remove course subjects
            coursesService.RemoveCourseSubjetcsByListOfSubjects(id, subjectsToRemove);

            // create List of subjects to expose
            List<Subject> subjectsToTrim = coursesService.GetSubjects(newSubjectList);

            List<SubjectToExpose> subjectsToExpose = coursesService.TrimSubjects(subjectsToTrim);

            // create Course to expose
            CourseToExpose courseToExpose = courseToExposeService
                .TrimUpdateCourse(id, courseCreator, subjectsToExpose);

            return Ok(courseToExpose);
        }

        // DELETE /api/courses/id
        public IHttpActionResult DeleteCourse(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            coursesService = new CoursesService();

            coursesService.DeleteCourseById(id);

            return Ok(coursesService.ListCourses().ToList().Select(Mapper.Map<Course, CourseDTO>));
        }
    }
}
