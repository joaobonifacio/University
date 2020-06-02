using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.Services
{
    public class HireTeacherSingleton
    {
        public List<Subject> Subjects = new List<Subject>();

        public Teacher TeacherToBeReplaced = new Teacher();
        public Subject SubjectWhereTeacherWillBeReplaced = new Subject();

        private static HireTeacherSingleton instance = null;

        public static HireTeacherSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HireTeacherSingleton();
                }
                return instance;
            }
        }

        public List<Subject> getSubjects()
        {
            return Subjects;
        }
    }
}