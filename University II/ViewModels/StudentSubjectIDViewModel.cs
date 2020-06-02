using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class StudentSubjectIDViewModel
    {
        public int SubjectID { get; set; }

        public StudentSubject StudentSubject { get; set; }
    }
}