using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace University_II.Models
{
    public class Subject
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Range(1,6)]
        [Required] 
        public int Credits { get; set; }

        public Teacher Teacher { get; set; }

        [Required] 
        public int TeacherId { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}