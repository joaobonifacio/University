using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class ChooseCourseSubjectsViewModel
    {
        public Course Course { get; set; }

        public Subject Subject { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }
    }
}