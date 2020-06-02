using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using University_II.Controllers;
using University_II.Models;

namespace University_II.Services
{
    public class CourseCreatorSingleton
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private SubjectService subjectService;
        private CourseService courseService;

        public string ClassName { get; set; }

        public List<int> SubjectIds = new List<int>();

        public List<int> allSubjectIds = new List<int>();

        public List<int> remainingSubjectIds = new List<int>();

        private static CourseCreatorSingleton instance = null;
       
        public static CourseCreatorSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CourseCreatorSingleton();
                }
                return instance;
            }
        }
        
        public List<int> getSubjectIds()
        {
            return SubjectIds;
        }

        public List<int> getAllSubjectIds()
        {
            return allSubjectIds;
        }

        public List<int> getRemainingSubjectIds()
        {
            return remainingSubjectIds;
        }

        public void AddSubjectToList(int subjectId) 
        {
            courseService = new CourseService();


            if (SubjectIds == null)
            {
                SubjectIds.Add(subjectId);
            }

            if(SubjectIds.Count() < 5)
            {
                SubjectIds.Add(subjectId);
            }
        }

        public List<int> getRemainingSubjectIds(int newSubjectId)
        {
            foreach(int subjectId in remainingSubjectIds)
            {
                if(subjectId == newSubjectId)
                {
                    remainingSubjectIds.Remove(newSubjectId);

                    return remainingSubjectIds;
                }
            }

            return null;
        }

        public Course CreateCourseAndCourseSubjectsAndSaveToDB()
        {
            //course is saved to db in this moment
            Course newCourse = courseService.CreateCourseByName(ClassName);

            return newCourse;
        }

        public IEnumerable<Subject> getListOfChosenSubjects()
        {
            subjectService = new SubjectService();

            IEnumerable<Subject> theSubjects = subjectService.getSubjectsByListOfId(SubjectIds);

            return theSubjects;
        }
    }
}