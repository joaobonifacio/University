using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class StudentAverageGradeViewModel
    {
        public Student student { get; set; }

        public Course course { get; set; }

        public Grade? grade { get; set; }
    }
}