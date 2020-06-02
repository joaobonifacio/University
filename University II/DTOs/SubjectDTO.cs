using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.DTOs
{
    public class SubjectDTO
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Range(1, 6)]
        [Required]
        public int Credits { get; set; }

        [Required]
        public int TeacherId { get; set; }
    }
}