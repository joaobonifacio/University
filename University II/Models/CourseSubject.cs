using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_II.Models
{
    public class CourseSubject
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int Id { get; set; }

        public int CourseId { get; set; }

        public int SubjectId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Subject Subject { get; set; }

        public List<CourseSubject> CreateCourseSubjectsBySubjectsIDAndCourse(Course course, List<int> subjectsIds)
        {
            List<CourseSubject> courseSubjects = new List<CourseSubject>();

            foreach(int subjectId in subjectsIds)
            {
                if(db.Subjects.Find(subjectId) == null)
                {
                    return null;
                }

                CourseSubject courseSubject = new CourseSubject()
                {
                    CourseId = course.Id,
                    SubjectId = subjectId
                };

                db.CourseSubjects.Add(courseSubject);
                db.SaveChanges();

                courseSubjects.Add(courseSubject);
            }

            return courseSubjects;
        }
    }
}