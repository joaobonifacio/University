namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToModelAndModelNames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.CourseSubjects", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseSubjects", new[] { "SubjectId" });
            DropIndex("dbo.CourseSubjects", new[] { "CourseId" });
            DropTable("dbo.CourseSubjects");
        }
    }
}
