namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedModelSubjectToYadaYadaYada : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubjectToCreateACourses", "Subject_ID", "dbo.Subjects");
            DropIndex("dbo.SubjectToCreateACourses", new[] { "Subject_ID" });
            DropTable("dbo.SubjectToCreateACourses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubjectToCreateACourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Subject_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.SubjectToCreateACourses", "Subject_ID");
            AddForeignKey("dbo.SubjectToCreateACourses", "Subject_ID", "dbo.Subjects", "ID");
        }
    }
}
