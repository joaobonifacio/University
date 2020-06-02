﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class TeacherStudentSubjectViewModel
    {

        public Teacher Teacher { get; set;  }

        public Subject Subject { get; set; }

        public StudentSubject StudentSubject { get; set; }
    }
}