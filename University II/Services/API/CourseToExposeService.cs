using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;
using University_II.Models.API;

namespace University_II.Services.API
{
    public class CourseToExposeService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CourseToExpose TrimSubject(List<CourseSubject> courseSubjects, CourseCreator courseCreator, List<SubjectToExpose> subjectsToExpose)
        {
            CourseToExpose courseToExpose = new CourseToExpose()
            {
                Id = courseSubjects.ToArray()[0].CourseId,
                Title = courseCreator.Title,
                SubjectsToExpose = subjectsToExpose
            };

            return courseToExpose;
        }

        public CourseToExpose TrimUpdateCourse(int id, CourseCreator courseCreator, List<SubjectToExpose> subjectsToExpose)
        {
            CourseToExpose courseToExpose = new CourseToExpose()
            {
                Id = id,
                Title = courseCreator.Title,
                SubjectsToExpose = subjectsToExpose
            };

            return courseToExpose;
        }
    }
}