using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_II.Models.API
{
    public class StudentToExpose
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int CourseId { get; set; }

        public bool IsEnrolled { get; set; }
    }
}