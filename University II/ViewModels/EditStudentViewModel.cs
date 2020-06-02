using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class EditStudentViewModel
    {
        public string  Name { get; set; }

        public string Email { get; set; }

        public int StudentId { get; set; }
    }
}