using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using University_II.Models;
using University_II.Models.API;
using University_II.Services;

namespace University_II.Services.API
{
    public class SubjectsService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TeachersService teachersService;
        private SubjectService subjectService;
        private SubjectToExposeService subjectToExposeService;

        public Subject UpdateSubject(int id, Subject subject)
        {
            Subject theSubject = db.Subjects.Find(id);
            
            if (theSubject == null)
            {
                return null;
            }
            
            // check if teacher id correspondes to teacher name
            if (subject.TeacherId == 0)
            {
                return null;
            }
            
            Teacher theTeacher = db.Teachers.Find(subject.TeacherId);
            
            if (theTeacher == null)
            {
                return null;
            }

            // check for element nulls 
            teachersService = new TeachersService();
            
            int previousTeacherId = theSubject.TeacherId;
            
            if (subject.Title == null)
            {
                return null;
            }
            else
            {
                theSubject.Title = subject.Title;
            }
            
            if (subject.Credits == 0)
            {
                return null;
            }
            else
            {
                theSubject.Credits = subject.Credits;
            }

            // check to see if teacher in subject is the previous teacher
            if (previousTeacherId == subject.TeacherId)
            {
                db.SaveChanges();

                return theSubject;
            }
            else 
            {
                List<Teacher> nonAllocatedTeachers = teachersService.GetNonAllocatedTeachers();
                List<int> nonAllocatedTeachersIds = new List<int>();

                foreach(Teacher nonAllocatedTeacher in nonAllocatedTeachers)
                {
                    nonAllocatedTeachersIds.Add(nonAllocatedTeacher.Id);
                }

                if (nonAllocatedTeachersIds.Contains(subject.TeacherId))
                {
                    theSubject.TeacherId = subject.TeacherId;
                    db.SaveChanges();

                    return theSubject;
                } 
            }
            
            return null;
        }

        public Subject CheckIfSubjectExistsByName(Subject subject)
        {
            subjectService = new SubjectService();

            return subjectService.getSubjectBySubjectTitle(subject.Title);
        }

        public Subject Find(int id)
        {
            subjectService = new SubjectService();

            return subjectService.getSubjectById(id);
        }

        public void DeleteSubject(int id)
        {
            subjectService = new SubjectService();

            subjectService.DeleteSubject(id);
        }

        public bool CheckIfSubjectExists(int id)
        {
            subjectService = new SubjectService();

            return subjectService.CheckIfSubjectExists(id);
        }

        public List<SubjectToExpose> GetSubjects()
        {
            subjectToExposeService = new SubjectToExposeService();

            return subjectToExposeService.TrimSubjects();
        }

        public Subject GetSubjectById(int id)
        {
            subjectService = new SubjectService();

            return subjectService.getSubjectById(id);
        }

        public Subject CreateSubject(Subject subject)
        {
            subjectService = new SubjectService();

            return subjectService.CreateSubject(subject);
        }
    }
}
