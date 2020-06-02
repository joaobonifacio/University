using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_II.Models.API
{
    public class SubjectToExpose
    {
        public int ID { get; set; }

        public string Title { get; set; }
        public int Credits { get; set; }

        public string TeacherName { get; set; }

        public int TeacherId { get; set; }
    }
}