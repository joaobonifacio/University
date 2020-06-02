using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_II.Models.API
{
    public class StudentSubjectToExpose
    {
        public int StudentSubjectID { get; set; }

        public int SubjectID { get; set; }

        public int StudentID { get; set; }

        public Grade? grade { get; set; }

    }
}