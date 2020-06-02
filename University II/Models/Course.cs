using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace University_II.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}