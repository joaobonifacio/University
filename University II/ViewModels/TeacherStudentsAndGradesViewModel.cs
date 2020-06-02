using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class TeacherStudentsAndGradesViewModel
    {
        public Teacher Teacher { get; set; }

        public int TeacherId { get; set; }

        public Subject Subject { get; set; }

        public Student Student { get; set; }

        public StudentSubject StudentSubject { get; set; }
}
}