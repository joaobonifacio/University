namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateUNiversityStudents : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
                "VALUES ('student1', 'student1@gmail.com', 1, 1, 0, 0)");

        }

        public override void Down()
        {
        }
    }
}
