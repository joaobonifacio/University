using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class CourseStudentSubjectViewModel
    {
        public Course Course { get; set; }

        public Student Student { get; set; }

        public Subject Subject { get; set; }

        public StudentSubject StudentSubject { get; set; }
    }
}