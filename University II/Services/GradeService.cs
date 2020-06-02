using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.Services
{
    public class GradeService: IService
    {
        List<T> IService.ListAll<T>()
        {
            throw new NotImplementedException();
        }

        public Grade? ConvertIntToGrade(int gradeToConvert)
        {
            Grade? grade;

            if (gradeToConvert == 1)
            {
                return grade = Grade.D;
            }
            else if (gradeToConvert == 2)
            {
                return grade = Grade.D;
            }
            else if (gradeToConvert == 3)
            {
                return grade = Grade.C;
            }
            else if (gradeToConvert == 4)
            {
                return grade = Grade.B;
            }
            else if( gradeToConvert == 5)
            {
                return grade = Grade.A;
            }

            return null;
        }


        public int ConvertGradeToInt(Grade? grade)
        {
            if (grade == Grade.A )
            {
                return 5;
            }
            else if (grade == Grade.B)
            {
                return 4;
            }
            else if (grade == Grade.C)
            {
                return 3;
            }
            else if (grade == Grade.D)
            {
                return 2;
            }
            else if (grade == Grade.E)
            {
                return 1;
            }

            return 0;
        }

        public int ConvertAllGradesToInt(List<Grade?> grades)
        {
            int totalGrades = 0;

            foreach (Grade? grade in grades)
            {
                if (grade == Grade.A)
                {
                    totalGrades += 5;
                }
                else if (grade == Grade.B)
                {
                    totalGrades += 4;
                }
                else if (grade == Grade.C)
                {
                    totalGrades += 3;
                }
                else if (grade == Grade.D)
                {
                    totalGrades += 2;
                }
                else if (grade == Grade.E)
                {
                    totalGrades += 1;
                }
            }


            return totalGrades;
        }
    }
}