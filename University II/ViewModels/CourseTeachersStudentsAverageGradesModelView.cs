using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class CourseTeachersStudentsAverageGradesModelView
    {
        public Course Course { get; set; }

        public int numberOfTeachers { get; set; }

        public int numberOfStudents { get; set; }

        public Grade? averageGrade { get; set; }
    }
}