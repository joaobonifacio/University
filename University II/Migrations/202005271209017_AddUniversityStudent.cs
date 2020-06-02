namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniversityStudent : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO UniversityStudentsLists (Name, Email, IdentificationCard, CourseId, isEnrolled, Birthday) " +
             "VALUES ('fourth', 'fourth@gmail.com', 1, 1, 0, 0)");
        }
        
        public override void Down()
        {
        }
    }
}
