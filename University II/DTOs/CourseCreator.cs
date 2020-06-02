using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace University_II.Models.API
{
    public class CourseCreator
    {
        public string Title { get; set; }

        public List<int> SubjectIds { get; set; }
    }
}