using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;
using University_II.Models.API;

namespace University_II.Services.API
{
    public class SubjectToExposeService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<SubjectToExpose> TrimSubjects()
        {
            List<SubjectToExpose> subjectsToExpose = new List<SubjectToExpose>();
            List<Subject> subjects = db.Subjects.ToList();
            string teacherName;

            foreach (Subject subject in subjects)
            {
                Teacher teacher = db.Teachers.Find(subject.TeacherId);
                if(teacher != null)
                {
                   teacherName = teacher.Name;
                }
                else
                {
                    teacherName = null;
                }

                SubjectToExpose subjectToExpose = new SubjectToExpose()
                {
                    ID = subject.ID,
                    Title = subject.Title,
                    Credits = subject.Credits,
                    TeacherId = subject.TeacherId,
                    TeacherName = teacherName
                };

                subjectsToExpose.Add(subjectToExpose);
            }

            return subjectsToExpose;
        }

        public SubjectToExpose TrimSubject(Subject subject)
        {
            SubjectToExpose subjectToExpose;
            string teacherName;
            
            Teacher teacher = db.Teachers.Find(subject.TeacherId);
            
            if (teacher != null)
            {
                teacherName = teacher.Name;
            }
            else
            {
                return null;
            }
            
            subjectToExpose = new SubjectToExpose()
            {
                ID = subject.ID,
                Title = subject.Title,
                Credits = subject.Credits,
                TeacherId = subject.TeacherId,
                TeacherName = teacherName
            };

            return subjectToExpose;
        }

        public List<SubjectToExpose> TrimSubjects(List<Subject> subjects)
        {
            List<SubjectToExpose> subjectsToExpose = new List<SubjectToExpose>();
            
            string teacherName;

            foreach (Subject subject in subjects)
            {
                Teacher teacher = db.Teachers.Find(subject.TeacherId);
                if (teacher != null)
                {
                    teacherName = teacher.Name;
                }
                else
                {
                    teacherName = null;
                }

                SubjectToExpose subjectToExpose = new SubjectToExpose()
                {
                    ID = subject.ID,
                    Title = subject.Title,
                    Credits = subject.Credits,
                    TeacherId = subject.TeacherId,
                    TeacherName = teacherName
                };

                subjectsToExpose.Add(subjectToExpose);
            }

            return subjectsToExpose;
        }

    }
}