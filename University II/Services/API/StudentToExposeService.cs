using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;
using University_II.Models.API;

namespace University_II.Services.API
{
    public class StudentToExposeService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public StudentToExpose TrimStudent(Student student)
        {
            StudentToExpose studentToExpose = new StudentToExpose()
            {
                Id = student.ID,
                Name = student.Name,
                Email = student.Email,
                CourseId = student.CourseId,
                IsEnrolled = student.isEnrolled
            };

            return studentToExpose;
        }
    }
}