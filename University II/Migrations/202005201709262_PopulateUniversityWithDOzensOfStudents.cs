namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateUniversityWithDOzensOfStudents : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student2', 'student2@gmail.com', 2, 1, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student3', 'student3@gmail.com', 3, 1, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student4', 'student4@gmail.com', 4, 1, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student5', 'student5@gmail.com', 5, 1, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student6', 'student6@gmail.com', 6, 1, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student7', 'student7@gmail.com', 7, 1, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student8', 'student8@gmail.com', 8, 1, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student9', 'student9@gmail.com', 9, 1, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student10', 'student10@gmail.com', 10, 1, 0, 0)");



            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student11', 'student11@gmail.com', 11, 2, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student12', 'student12@gmail.com', 12, 2, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student13', 'student13@gmail.com', 13, 2, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student14', 'student14@gmail.com', 14, 2, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student15', 'student15@gmail.com', 15, 2, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student16', 'student16@gmail.com', 16, 2, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student17', 'student17@gmail.com', 17, 2, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student18', 'student18@gmail.com', 18, 2, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student19', 'student19@gmail.com', 19, 2, 0, 0)");

            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student20', 'student20@gmail.com', 20, 2, 0, 0)");




        }
        
        public override void Down()
        {
        }
    }
}
