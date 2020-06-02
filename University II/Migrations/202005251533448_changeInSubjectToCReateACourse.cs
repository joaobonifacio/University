namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeInSubjectToCReateACourse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubjectToCreateACourses", "SubjectID", "dbo.Subjects");
            DropIndex("dbo.SubjectToCreateACourses", new[] { "SubjectID" });
            RenameColumn(table: "dbo.SubjectToCreateACourses", name: "SubjectID", newName: "Subject_ID");
            AlterColumn("dbo.SubjectToCreateACourses", "Subject_ID", c => c.Int());
            CreateIndex("dbo.SubjectToCreateACourses", "Subject_ID");
            AddForeignKey("dbo.SubjectToCreateACourses", "Subject_ID", "dbo.Subjects", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectToCreateACourses", "Subject_ID", "dbo.Subjects");
            DropIndex("dbo.SubjectToCreateACourses", new[] { "Subject_ID" });
            AlterColumn("dbo.SubjectToCreateACourses", "Subject_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.SubjectToCreateACourses", name: "Subject_ID", newName: "SubjectID");
            CreateIndex("dbo.SubjectToCreateACourses", "SubjectID");
            AddForeignKey("dbo.SubjectToCreateACourses", "SubjectID", "dbo.Subjects", "ID", cascadeDelete: true);
        }
    }
}
