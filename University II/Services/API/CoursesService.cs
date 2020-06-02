using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;
using University_II.Models.API;

namespace University_II.Services.API
{
    public class CoursesService
    {
        private CourseService courseService;
        private CourseSubjectService courseSubjectService;
        private SubjectToExposeService subjectToExposeService;
        private SubjectService subjectService;

        public IEnumerable<Course> ListCourses()
        {
            courseService = new CourseService();

            return courseService.ListCourses();
        }

        public Course GetCourseByCourseId(int id)
        {
            courseService = new CourseService();

            return courseService.GetCourseByCourseId(id);
        }

        public List<CourseSubject> CreateCourse(CourseCreator courseCreator)
        {
            courseService = new CourseService();

            List<CourseSubject> courseSubjects = courseService.CreateCourse(courseCreator);

            return courseSubjects;
        }

        public Course ChangeCourseTitleById(int id, string title)
        {
            courseService = new CourseService();

            return courseService.ChangeCourseTitleById(id, title);
        }

        public void DeleteCourseById(int id)
        {
            courseService = new CourseService();

            courseService.DeleteCourseById(id);
        }

        public bool CheckIfCourseAlreadyExists(string title)
        {
            bool exists = false;

            courseService = new CourseService();

            exists = courseService.CheckIfCourseAlreadyExists(title);

            return exists;
        }

        public List<int> GetCourseSubjectsIds(Course courseToUpdate)
        {
            courseService = new CourseService();

            List<int> courseSubjectsIds = courseService.GetCourseSubjectsIds(courseToUpdate);

            return courseSubjectsIds;
        }

        public List<CourseSubject> CreateCourseSubjectsByIdAndListOfSubjects(int id, List<int> subjectsToAdd)
        {
            courseSubjectService = new CourseSubjectService();

            List<CourseSubject> newCourseSubjects = courseSubjectService
                .CreateCourseSubjectsByIdAndListOfSubjects(id, subjectsToAdd);

            return newCourseSubjects;
        }

        public List<SubjectToExpose> TrimSubjects(List<Subject> subjects)
        {
            subjectToExposeService = new SubjectToExposeService();

            List<SubjectToExpose> subjectsToExpose = subjectToExposeService.TrimSubjects(subjects);

            return subjectsToExpose;
        }

        public List<Subject> GetSubjects(List<int> newSubjectList)
        {
            subjectService = new SubjectService();

            IEnumerable<Subject> subjects = subjectService.getSubjectsByListOfId(newSubjectList);

            return subjects.ToList();
        }

        public Course UpdateTitle(int id, string title)
        {
            Course course = courseService.UpdateTitle(id, title);

            return course;
        }

        public void RemoveCourseSubjetcsByListOfSubjects(int id, List<int> subjectsToRemove)
        {
            courseSubjectService.RemoveCourseSubjetcsByListOfSubjects(id, subjectsToRemove);
        }
    }
}