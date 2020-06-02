﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class TeacherSubjectNumberOfStudentsViewModel
    {
        public Teacher Teacher { get; set; }
        
        public Subject Subject { get; set; }

        public int NumberOfStudents { get; set; }
    }
}