namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateOneSubjectOfPhysicsInCourseSubjetcs : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (2, 1)");
        }

        public override void Down()
        {
        }
    }
}
