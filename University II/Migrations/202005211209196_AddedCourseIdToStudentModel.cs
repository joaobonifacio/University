namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourseIdToStudentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "CourseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "CourseId");
        }
    }
}
