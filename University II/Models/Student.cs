using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace University_II.Models
{
    public class Student
    {
        public int ID { get; set; } 

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; } 

        public int CourseId { get; set; }

        public bool isEnrolled { set; get; }
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        
    }
}