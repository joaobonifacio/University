using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class HireNewTeacherViewModel
    {
        public Teacher Teacher { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}