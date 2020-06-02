using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class UniversityStudentCreationViewModel
    {
        public UniversityStudentsList Student { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}