using System;

namespace University_II.Models
{
    public enum Grade
        {
            E, D, C, B, A
        } 
    
    public class StudentSubject 
    { 
        public int StudentSubjectID { get; set; } 
        public int SubjectID { get; set; } 
        public int StudentID { get; set; } 
        public Grade? Grade { get; set; }
        public virtual Subject Subject{ get; set; } 
        public virtual Student Student { get; set; }

    }
}