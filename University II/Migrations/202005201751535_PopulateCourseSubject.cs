namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCourseSubject : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (1, 1)");
        }

        public override void Down()
        {
        }
    }
}
