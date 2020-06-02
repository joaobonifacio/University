namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateThe4RemainingSubjectsInFirstCourseInCourseSubjects : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (1, 2)");

            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (1, 3)");
            
            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (1, 5)");
            
            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (1, 8)");

        }

        public override void Down()
        {
        }
    }
}
