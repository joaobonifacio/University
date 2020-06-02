using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace University_II.DTOs
{
    public class UniversityStudentListDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int IdentificationCard { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public bool isEnrolled { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}