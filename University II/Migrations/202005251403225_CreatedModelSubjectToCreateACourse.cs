namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedModelSubjectToCreateACourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubjectToCreateACourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectToCreateACourses", "SubjectID", "dbo.Subjects");
            DropIndex("dbo.SubjectToCreateACourses", new[] { "SubjectID" });
            DropTable("dbo.SubjectToCreateACourses");
        }
    }
}
