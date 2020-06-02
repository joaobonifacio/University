using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using University_II.Models;

namespace University_II.ViewModels
{
    public class TeacherSubjectViewModel
    {
        public Course Course { get; set; }

        public Subject Subject { get; set; }

        public Teacher Teacher { get; set; }

        public IEnumerable<Teacher> Teachers { get; set; }
    }
}