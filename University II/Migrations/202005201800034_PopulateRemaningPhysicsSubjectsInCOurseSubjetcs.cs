namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateRemaningPhysicsSubjectsInCOurseSubjetcs : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (2, 6)");

            Sql("INSERT INTO CourseSubjects (CourseId, SubjectId) VALUES (2, 10)");
        }
        
        public override void Down()
        {
        }
    }
}
