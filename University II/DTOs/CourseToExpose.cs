using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_II.Models.API
{
    public class CourseToExpose
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<SubjectToExpose> SubjectsToExpose { get; set; }
    }
}