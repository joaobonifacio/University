using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using University_II.Models;

namespace University_II.Services
{
    public class UniversityStudentsListService: IService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private StudentRegisterService studentRegisterService;
        private CourseService courseService;
        private StudentService studentService;

        List<T> IService.ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public List<UniversityStudentsList> GetUniversityStudentsLists()
        {
            return db.UniversityStudentsList.ToList();
        }

        public string CheckIfIsStudentAndRegisterIt(ApplicationUser user)
        {
            IEnumerable<UniversityStudentsList> universityStudent = db.UniversityStudentsList.ToList();
            studentRegisterService = new StudentRegisterService();

            foreach (UniversityStudentsList student in universityStudent)
            {
                if (user.Email == student.Email)
                {
                    // this will add and save student to the db
                    studentRegisterService.RegisterStudent(student);

                    return "Student";
                }
            }

            return null;
        }

        public bool CheckUniversityStudentModel(UniversityStudentsList uniStudent)
        {
            courseService = new CourseService();

            if (uniStudent.Name == null || uniStudent.Email == null ||
                uniStudent.IdentificationCard == 0 || uniStudent.Birthday == null
                || courseService.GetCourseByCourseId(uniStudent.CourseId) == null)
            {
                return false;
            }

            return true;
        }

        public bool CheckIfStudentAlreadyExists(UniversityStudentsList uniStudent)
        {
            IEnumerable<UniversityStudentsList> emails = db.UniversityStudentsList
                .Where(u => u.Email == uniStudent.Email).ToList();

            if (emails.Count() > 0)
            {
                return false;
            }

            IEnumerable<UniversityStudentsList> identificationCards = db.UniversityStudentsList
                .Where(u => u.IdentificationCard == uniStudent.IdentificationCard).ToList();

            if (identificationCards.Count() > 0)
            {
                return false;
            }

            return true;
        }

        public void ChangeNameByStudentId(string email, string name)
        {
            studentService = new StudentService();

            IEnumerable<UniversityStudentsList> uniStudents = db.UniversityStudentsList
                .Where(u => u.Email == email);

            if(uniStudents.ToList().Count > 0)
            {
                UniversityStudentsList uniStudent = uniStudents.ToArray()[0];
                uniStudent.Name = name;

                db.SaveChanges();
            }
        }

        public UniversityStudentsList UpdateUniversityStudent(int id, UniversityStudentsList uniStudent)
        {
            UniversityStudentsList studentToUpdate = db.UniversityStudentsList.Find(id);

            if (!CheckUniversityStudentModel(uniStudent))
            {
                return null;
            }

            // if model is ok, populate UNiversityStudent and save it to db
            UniversityStudentsList universityStudent = PopulateUniversityStudent(studentToUpdate, uniStudent);

            return studentToUpdate;
        }

        public bool CheckIfStudentExistsInUniversityListDb(int id)
        {
            UniversityStudentsList uniStudent = db.UniversityStudentsList.Find(id);

            if(uniStudent == null)
            {
                return false;
            }

            return true;
        }

        public UniversityStudentsList PopulateUniversityStudent(UniversityStudentsList StudentToUpdate, UniversityStudentsList uniStudent)
        {
            UniversityStudentsList universityStudent = db.UniversityStudentsList.Find(StudentToUpdate.Id);

            universityStudent.Name = uniStudent.Name;
            universityStudent.Email = uniStudent.Email;
            universityStudent.IdentificationCard = uniStudent.IdentificationCard;
            universityStudent.CourseId = uniStudent.CourseId;
            universityStudent.Birthday = uniStudent.Birthday;

            db.SaveChanges();

            return universityStudent;
        }

        public void AddStudentToUniversityStudentListAndSaveToDb(UniversityStudentsList universityStudent)
        {
            db.UniversityStudentsList.Add(universityStudent);
            db.SaveChanges();
        }

        public UniversityStudentsList CreateUniversityStudentList(UniversityStudentsList uniStudent)
        {
            courseService = new CourseService();
            UniversityStudentsList universityStudent = new UniversityStudentsList();

            // check university student model
            bool modelIsOK = CheckUniversityStudentModel(uniStudent);

            if (!modelIsOK)
                return null;
            

            //Check if student already exists
            bool studentAlreadyExists = CheckIfStudentAlreadyExists(uniStudent);

            if (studentAlreadyExists)
                return null;


            // if model is ok, populate UNiversityStudent
            universityStudent = PopulateUniversityStudent(universityStudent, uniStudent);

            // add student to university student list and save to db
            AddStudentToUniversityStudentListAndSaveToDb(universityStudent);

            return universityStudent;
        }

        public Student CheckIfUniversityStudentExistsInStudentsDB(UniversityStudentsList uniStudent)
        {
            // compare emails
            IEnumerable<Student> students = db.Students.Where(s => s.Email == uniStudent.Email).ToList();

            if(students.Count() == 0)
            {
                return null;
            }

            return students.ToArray()[0];
        }

        public UniversityStudentsList GetUniversityStudentsList(int id)
        {
            UniversityStudentsList uniStudent = new UniversityStudentsList();

            uniStudent = db.UniversityStudentsList.Find(id);

            if(uniStudent == null)
                return null;

            return uniStudent;
                
        }

        public void UpdateUniversityStudentEnrollment(UniversityStudentsList student)
        {
            UniversityStudentsList newUniversityStudent;

            IEnumerable<UniversityStudentsList> universityStudents = db.UniversityStudentsList
                .Where(u => u.Email == student.Email).ToList();

            newUniversityStudent = universityStudents.ToArray()[0];
            newUniversityStudent.isEnrolled = true;
            db.SaveChanges();
        }

        public int GetNumberOfStudentsEnrolledInACourse(Course course)
        {
            int count = 0;
            List<UniversityStudentsList> universityStudents = db.UniversityStudentsList.ToList();

            foreach (UniversityStudentsList universityStudentList in universityStudents)
            {
                if (universityStudentList.CourseId == course.Id)
                {
                    if (universityStudentList.isEnrolled)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public void AddNewStudentToUniversityStudentsLists(UniversityStudentsList student)
        {
            db.UniversityStudentsList.Add(student);
            db.SaveChanges();

            bool enrolled = student.isEnrolled;
        }

        public List<Student> GetStudentsEnrolledInACourse(Course course)
        {
            IEnumerable<string> studentsEnrolledInACourseEmails = from u in db.UniversityStudentsList
                                                      join c in db.Courses
                                                      on u.CourseId equals course.Id
                                                      where u.isEnrolled 
                                                      select u.Email;

            IEnumerable<Student> studentsRegisteredInACourse = from e in studentsEnrolledInACourseEmails
                                                               join s in db.Students
                                                               on e equals s.Email
                                                               select s;


            return studentsRegisteredInACourse.ToList();
        }

        public void RemoveStudentFromUniversityStudentList(Student student)
        {
            IEnumerable<UniversityStudentsList> universityStudent = new List<UniversityStudentsList>();

            universityStudent = from s in db.Students
                                join u in db.UniversityStudentsList
                                on s.Email equals u.Email
                                select u;

            db.UniversityStudentsList.Remove(universityStudent.ToArray()[0]);
            db.SaveChanges();
        }

        public void SetIsEnrolledToFalseByStudentId(int id)
        {
            List<Student> students = db.Students.Where(s => s.ID == id).ToList();

            Student student = students.ToArray()[0];

            List<UniversityStudentsList> universityStudents = db.UniversityStudentsList
                .Where(u => u.Email == student.Email).ToList();

            UniversityStudentsList universityStudent = universityStudents.ToArray()[0];

            universityStudent.isEnrolled = false;

            db.Entry(universityStudent).State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}