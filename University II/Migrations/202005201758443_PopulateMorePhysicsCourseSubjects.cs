namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMorePhysicsCourseSubjects : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (2, 2)");

            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (2, 3)");
        }

        public override void Down()
        {
        }
    }
}
