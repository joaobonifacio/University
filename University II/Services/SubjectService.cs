using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web;
using System.Web.Http;
using University_II.Models;
using University_II.ViewModels;

namespace University_II.Services
{
    public class SubjectService : Services.IService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TeacherService teacherService;

        public List<T> ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public List<Subject> getAllSubjects()
        {
            return db.Subjects.ToList();
        }

        public Subject getSubjectById(int subjectId)
        {
            Subject subject = db.Subjects.Find(subjectId);

            return subject;
        }

        public List<Subject> TrimSubjects()
        {
            List<Subject> subjectsToExpose = new List<Subject>();

            foreach(Subject subject in db.Subjects)
            {
                Subject subjectToExpose = new Subject()
                {
                    ID = subject.ID,
                    Credits = subject.Credits,
                    Teacher = subject.Teacher,
                    TeacherId = subject.TeacherId,
                    Title = subject.Title
                };

                subjectsToExpose.Add(subjectToExpose);
            }

            return subjectsToExpose;
        }

        public List<TeacherSubjectNumberOfStudentsViewModel> CreateTeacherSubjectNumberOfStudentsViewModel(Subject[] subjects, Teacher[] teachers, int[] students)
        {
            Subject[] allSubjectsArray = subjects;
            Teacher[] teachersArray = teachers;
            int[] numberOfStudentsEnrolledArray = students;

            List<TeacherSubjectNumberOfStudentsViewModel> viewModel = new List<TeacherSubjectNumberOfStudentsViewModel>();

            for (int i = 0; i < allSubjectsArray.Length; i++)
            {
                TeacherSubjectNumberOfStudentsViewModel model = new TeacherSubjectNumberOfStudentsViewModel()
                {
                    Subject = allSubjectsArray[i],
                    Teacher = teachersArray[i],
                    NumberOfStudents = numberOfStudentsEnrolledArray[i]
                };

                viewModel.Add(model);
            }

            return viewModel;
        }

        public List<TeacherStudentSubjectViewModel> CreateTeacherStudentSubjectViewModel(Subject subject, Teacher teacher, IEnumerable<StudentSubject> studentSubjects)
        {
            List<TeacherStudentSubjectViewModel> viewModel = new List<TeacherStudentSubjectViewModel>();

            // in the case the subject has no students
            if(studentSubjects == null || studentSubjects.Count() == 0)
            {
                TeacherStudentSubjectViewModel model = new TeacherStudentSubjectViewModel()
                {
                    Subject = subject,
                    Teacher = teacher,
                };

                viewModel.Add(model);
            }
            else
            {
                // in the case the subject has students
                foreach (StudentSubject studentSubject in studentSubjects)
                {
                    TeacherStudentSubjectViewModel model = new TeacherStudentSubjectViewModel()
                    {
                        Subject = subject,
                        Teacher = teacher,
                        StudentSubject = studentSubject
                    };

                    viewModel.Add(model);
                }
            }
            
            return viewModel;
        }

        public TeacherSubjectViewModel CreateTeacherSubjectViewModel(Subject subject, Teacher teacher, IEnumerable<Teacher> teachers)
        {
            TeacherSubjectViewModel viewModel = new TeacherSubjectViewModel()
            {
                Subject = subject,
                Teacher = teacher,
                Teachers = teachers
            };

            return viewModel;
        }

        public Subject CreateSubject(Subject subject)
        {
            if(subject == null)
            {
                return null;
            }

            teacherService = new TeacherService();

            List<Teacher> nonAllocatedTeachers = teacherService.GetNonAllocatedTeachers();

            foreach(Teacher nonAllocatedTeacher in nonAllocatedTeachers)
            {
                if(subject.TeacherId == nonAllocatedTeacher.Id)
                {
                    db.Subjects.Add(subject);
                    db.SaveChanges();

                    return subject;
                }
            }

            // if everything else fails the subject is given to the first non-allocated teacher
            subject.TeacherId = nonAllocatedTeachers.ToArray()[0].Id;
            db.Subjects.Add(subject);
            db.SaveChanges();

            return subject;
        }

        public IEnumerable<Subject> getSubjectsByListOfId(List<int> subjectIds)
        {
            IEnumerable<Subject> theSubjects = from si in subjectIds
                                        join s in db.Subjects.ToList()
                                        on si equals s.ID
                                        select s;
            
            
            return theSubjects;
        }

        public void DeleteSubject(int id)
        {
            Subject subject = db.Subjects.Find(id);

            if(subject != null)
            {
                db.Subjects.Remove(subject);
                db.SaveChanges();
            }
        }

        public bool CheckIfSubjectExists(int id)
        {
            bool exists = false;

            return exists = (db.Subjects.Find(id) != null) ? true : false;
        }

        public Subject getSubjectBySubjectTitle(string subjectTitle)
        {
            IEnumerable<Subject> subjectList = db.Subjects.ToList();
            Subject subjectToFind = new Subject();

            foreach (Subject subject in subjectList)
            {
                if (subject.Title == subjectTitle)
                {
                    subjectToFind = subject;
                }
            }

            return subjectToFind;
        }

        public IEnumerable<Subject> GetSubjectsFromListOfStudentSubjects(List<StudentSubject> studentSubjects)
        {
            IEnumerable<Subject> theSubjetcs = from ss in studentSubjects
                                        join s in db.Subjects
                                        on ss.SubjectID equals s.ID
                                        select s;

            return theSubjetcs;
        }

        public void EditSubject(TeacherSubjectViewModel viewModel)
        {
            Subject subject = db.Subjects.Find(viewModel.Subject.ID);
            subject.Credits = viewModel.Subject.Credits;
            subject.Title = viewModel.Subject.Title;
            subject.TeacherId = viewModel.Subject.TeacherId;


            db.Entry(subject).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<Subject> getSubjectsByTeacherId(int teacherId)
        {
            List<Subject> subjects = new List<Subject>();

            foreach (Subject subject in db.Subjects)
            {
                if (subject.TeacherId == teacherId)
                {
                    subjects.Add(subject);
                }
            }

            return subjects;
        }

        public void DeleteSubjectByTeacherSubjectViewModel(TeacherSubjectViewModel viewModel)
        {
            Subject subject = db.Subjects.Find(viewModel.Subject.ID);
            db.Subjects.Remove(subject);
            db.SaveChanges();
        }

        public bool CheckIfTeacherHasSubject(int teacherId)
        {
            teacherService = new TeacherService();

            return teacherService.CheckIfTeacherHasSubject(teacherId);
        }

        public List<Teacher> GetNonAllocatedTeachers()
        {
            teacherService = new TeacherService();

            List<Teacher> nonAllocatedTeachers = teacherService.GetNonAllocatedTeachers();

            return nonAllocatedTeachers;
        }

        public Subject getTeacherSubjectByTeacherId(int teacherId)
        {

            foreach(Subject subject in db.Subjects.ToList())
            {
                if(subject.TeacherId == teacherId)
                {
                    return subject;
                }
            }
           
            return null;
        }

        public void CreateNewSubjectAndSaveIt(TeacherSubjectViewModel viewModel, Teacher chosenTeacher)
        {
            Subject theSubject = new Subject()
            {
                Credits = viewModel.Subject.Credits,
                Title = viewModel.Subject.Title,
                TeacherId = chosenTeacher.Id
            };

            db.Subjects.Add(theSubject);
            db.SaveChanges();
        }

        public void CreateNewSubjectAndSaveIt(TeacherSubjectViewModel viewModel)
        {
            Subject theSubject = new Subject()
            {
                Credits = viewModel.Subject.Credits,
                Title = viewModel.Subject.Title,
                TeacherId = viewModel.Teacher.Id
            };

            db.Subjects.Add(theSubject);
            db.SaveChanges();
        }

        public Subject GetSubjectByStudentSubjectID(int studentSubjectID)
        {
            StudentSubject studentSubject = db.StudentSubjects.Find(studentSubjectID);

            int subjectId = studentSubject.SubjectID;

            return db.Subjects.Find(subjectId);
        }

        public List<Subject> GetSubjectsFromACourse(Course course)
        {
            List<CourseSubject> courseSubjectsFromACourse = db.CourseSubjects
                .Where(cs => cs.CourseId == course.Id).ToList();

            IEnumerable<Subject> subjectsFromACourse = from cs in courseSubjectsFromACourse
                                                       join s in db.Subjects
                                                       on cs.SubjectId equals s.ID
                                                       select s;

            return subjectsFromACourse.ToList();
        }

        public void ChangeTeacherInAListaOfSubjects(List<Subject> subjectsList, Teacher teacher)
        {
            foreach(Subject subject in subjectsList)
            {
                subject.TeacherId = teacher.Id;

                db.Entry(subject).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void ChangeSubjectTeacher(Subject subjectWithNewTeacher, Teacher newTeacher)
        {
            Subject subjectToChangeTeacher = db.Subjects.Find(subjectWithNewTeacher.ID);

            subjectToChangeTeacher.TeacherId = newTeacher.Id;
            db.SaveChanges();
        }

        public bool ChangeCredits(int iD, int credits)
        {
            Subject subject = db.Subjects.Find(iD);

            if (subject == null)
                return false;

            subject.Credits = credits;
            db.SaveChanges();

            return true;
        }
    }
}