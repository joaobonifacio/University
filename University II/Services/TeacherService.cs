using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Http;
using University_II.Models;
using University_II.ViewModels;

namespace University_II.Services
{
    public class TeacherService : Services.IService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private StudentSubjectService studentSubjectService;
        private SubjectService subjectService;
        private StaffService staffService;

        public List<Teacher> GetListJustOfTeachers()
        {
            return db.Teachers.ToList();
        }

        private CourseSubjectService courseSubjectService;

        List<T> IService.ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public List<TeacherSubjectPairsViewModel> GetListOfTeachers()
        {
            subjectService = new SubjectService();

            List<Teacher> teachers = new List<Teacher>();
            teachers = db.Teachers.ToList();

            List<TeacherSubjectPairsViewModel> viewModel = new List<TeacherSubjectPairsViewModel>();

            // get corresponding subjects
            foreach (Teacher teacher in teachers)
            {
                Subject teachersSubject = subjectService.getTeacherSubjectByTeacherId(teacher.Id);

                TeacherSubjectPairsViewModel model = new TeacherSubjectPairsViewModel()
                {
                    Teacher = teacher,
                    Subject = teachersSubject
                };

                viewModel.Add(model);
            }

            return viewModel;

        }

        public Teacher CreateNewTeacher(Teacher teacher)
        {
            if(teacher != null)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
            }

            return teacher;
        }

        public bool CheckIfTeacherAlreadyExists(Teacher teacher)
        { 
            bool exists = false;

            IEnumerable<Teacher> theTeacher = db.Teachers.Where(t => t.Name == teacher.Name);

            if (theTeacher.Any())
                exists = true;

            IEnumerable<Teacher> newTeacher = db.Teachers.Where(t => t.Email == teacher.Email);

            if (newTeacher.Any())
                exists = true;

            return exists;
        }

        public List<TeacherStudentsAndGradesViewModel> CreateTeacherStudentsAndGradesViewModel(Teacher teacher, IEnumerable<StudentSubject> teacherStudentSubjects, IEnumerable<Student> teachersStudents, IEnumerable<Subject> teachersSubjects)
        {
            StudentSubject[] teacherStudentSubjectsArray = teacherStudentSubjects.ToArray();
            Subject[] teachersSubjectsArray = teachersSubjects.ToArray();
            Student[] teachersStudentsArray = teachersStudents.ToArray();

            List<TeacherStudentsAndGradesViewModel> viewModel = new List<TeacherStudentsAndGradesViewModel>();

            for (int i = 0; i < teacherStudentSubjectsArray.Length; i++)
            {
                TeacherStudentsAndGradesViewModel model = new TeacherStudentsAndGradesViewModel()
                {
                    StudentSubject = teacherStudentSubjectsArray[i],
                    Teacher = teacher,
                    TeacherId = teacher.Id,
                    Subject = teachersSubjectsArray[i],
                    Student = teachersStudentsArray[i]
                };

                viewModel.Add(model);
            }

            return viewModel;
        }

        public void DeleteTeacherAndSubstituteHim(int id)
        {
            staffService = new StaffService();

            Teacher newTeacher = new Teacher();

            newTeacher = GetNonAllocatedTeachers().ToArray()[0];

            IEnumerable<Subject> subject = from t in db.Teachers
                              join s in db.Subjects
                              on id equals s.TeacherId
                              select s;

            Subject teacherSubject = subject.ToArray()[0];

            teacherSubject.TeacherId = newTeacher.Id;
            db.SaveChanges();

            staffService.DeleteTeacherFromStaffById(id);

            DeleteTeacherById(id);
        }

        public bool CheckIfTeacherHasSubject(int id)
        {
            bool teacherHasSubject = false;
            
            IEnumerable<Subject> subject = db.Subjects.Where(s => s.TeacherId == id);

            if (subject.Any())
                teacherHasSubject = true;

            return teacherHasSubject;
        }

        public Teacher getCorrespondingTeacher(int TeacherId)
        {
            Teacher teacher = db.Teachers.Find(TeacherId);

            return teacher;
        }

        public List<TeacherSubjectNumberOfStudentsViewModel> CreateTeacherSubjectNumberOfStudentsViewModel(Teacher teacher, IEnumerable<Subject> teacherSubjects)
        {
            int numberOfStudents = 0;
            
            List<TeacherSubjectNumberOfStudentsViewModel> viewModel = 
                new List<TeacherSubjectNumberOfStudentsViewModel>();

            foreach (Subject subject in teacherSubjects)
            {
                numberOfStudents = studentSubjectService.NumberOfStudentsPerSubject(subject);

                TeacherSubjectNumberOfStudentsViewModel model =
                    new TeacherSubjectNumberOfStudentsViewModel()
                    {
                        Teacher = teacher,
                        Subject = subject,
                        NumberOfStudents = numberOfStudents
                    };

                viewModel.Add(model);
            }

            return viewModel;
        }

        public int GetTeacherIdFromEmail(string email)
        {
            List<Teacher> allTeachers = db.Teachers.ToList();

            foreach (Teacher teacher in allTeachers)
            {
                if (teacher.Email == email)
                {
                    return teacher.Id;
                }
            }

            return 0;
        }

        public Teacher UpdateTeacher(int id, Teacher teacher)
        {
            Teacher theTeacher = getCorrespondingTeacher(id);

            // can't change the email because it's necessary for user role
            //theTeacher.Email = teacher.Email;

            theTeacher.Name = teacher.Name;
            theTeacher.Salary = teacher.Salary;

            db.SaveChanges();

            return theTeacher;
        }

        public int GetNumberOfDifferentTeachersFromAListOfSubjects(List<Subject> subjects)
        {
            List<Subject> theSubjects = new List<Subject>();
                theSubjects = subjects;
            
                List<Teacher> theTeachers = new List<Teacher>();

            foreach (Subject subject in subjects)
            {
                if (!theTeachers.Any())
                {
                    theTeachers.Add(subject.Teacher);
                }
                else
                {
                    bool isInList = CheckIfTeacherAlreadyIsInList(subject, theTeachers);

                    if (!isInList)
                    {
                        theTeachers.Add(subject.Teacher);
                    }
                }
            }

            return theTeachers.Count();
        }

        public int GetNumberOfTeachersPerCourse(Course course)
        {
            courseSubjectService = new CourseSubjectService();

            List<Subject> subjectsPerCourse = courseSubjectService.GetSubjectsFromCourse(course.Id);

            IEnumerable<Teacher> teacherPerCourse = from s in subjectsPerCourse
                                                    join t in db.Teachers
                                                    on s.TeacherId equals t.Id
                                                    select t;

            return teacherPerCourse.ToList().Count();
        }

        private bool CheckIfTeacherAlreadyIsInList(Subject subject, List<Teacher> theTeachers)
        {
            Teacher teacherToCheck = new Teacher();

            teacherToCheck = subject.Teacher;

            if (theTeachers.Contains(teacherToCheck))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Teacher> GetTeacherNamesFromListOfSubjects(IEnumerable<Subject> subjects)
        {
            IEnumerable<Teacher> teachers = from s in subjects
                                                join t in db.Teachers.ToList()
                                                on s.TeacherId equals t.Id
                                                select t;

            return teachers;
        }

        public Teacher GetTeacherBySubjectId(int Id)
        {
            Subject subject = db.Subjects.Find(Id);
            Teacher teacher = db.Teachers.Find(subject.TeacherId);


            return teacher;
        }

        public Teacher GetTeacherById(int Id)
        {
            Teacher teacher = db.Teachers.Find(Id);

            return teacher;
        }

        public void AddAndSaveNewTeacher(Teacher teacher)
        {
            staffService = new StaffService();

            if(db.StaffMembers.Where(s => s.Email == teacher.Email).ToList().Count() == 0)
            {
                staffService.AddTeacherToStaffMember(teacher);
            }

            db.Teachers.Add(teacher);
            db.SaveChanges();
        }

        public IEnumerable<StudentSubject> GetTeacherStudentSubjectsByTeacherId(int teacherId)
        {
            studentSubjectService = new StudentSubjectService();

            IEnumerable<StudentSubject> teachersStudentSubjects = studentSubjectService.
               GetStudentSubjectsByTeacherId(teacherId);

            return teachersStudentSubjects;
        }

        public Teacher GetTeacherByStudentSubjectId(int studentSubjectID)
        {
            subjectService = new SubjectService();

            Subject subject = subjectService.GetSubjectByStudentSubjectID(studentSubjectID);

            int teacherId = subject.TeacherId;

            return db.Teachers.Find(teacherId);
        }

        public IEnumerable<Subject> GetTeachersSubjectsFromTeachersStudentSubjectList(IEnumerable<StudentSubject> teacherStudentSubjects)
        {
            IEnumerable<Subject> teachersSubjects = from t in teacherStudentSubjects
                                                    join s in db.Subjects
                                                    on t.SubjectID equals s.ID
                                                    select s;

            return teachersSubjects;
        }

        public void EditTeacher(Teacher updatedTeacher, int teacherToUpdateId)
        {
            Teacher oldTeacher = db.Teachers.Find(teacherToUpdateId);

            oldTeacher.Salary = updatedTeacher.Salary;
            oldTeacher.Name = updatedTeacher.Name;
            oldTeacher.Email = updatedTeacher.Email;

            db.SaveChanges();
        }

        public void DeleteTeacherById(int id)
        {
            if(id == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Teacher teacher = db.Teachers.Find(id);

            if(teacher == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            db.Teachers.Remove(teacher);
            db.SaveChanges();
        }

        public List<Teacher> GetAllocatedTeachers()
        {
            IEnumerable<Teacher> allocatedTeachers = from t in db.Teachers
                                              join s in db.Subjects
                                              on t.Id equals s.TeacherId
                                              select t;

            return allocatedTeachers.ToList();
        }

        public List<Teacher> GetNonAllocatedTeachers()
        {
            List<Subject> allSubjects = db.Subjects.ToList();
            List<Teacher> allTeachers = db.Teachers.ToList();
            List<Teacher> nonAllocatedTeachers = new List<Teacher>();


            foreach(Teacher teacher in allTeachers)
            {
                int count = 0;

                foreach (Subject subject in allSubjects)
                {
                    if(subject.TeacherId == teacher.Id)
                    {
                        count++;
                    }
                }

                if(count == 0)
                {
                    nonAllocatedTeachers.Add(teacher);
                }

            }


            return nonAllocatedTeachers.ToList();
        }
    }
}