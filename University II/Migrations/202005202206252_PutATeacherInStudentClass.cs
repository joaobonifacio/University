namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PutATeacherInStudentClass : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Subjects", "TeacherId");
            AddForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Subjects", new[] { "TeacherId" });
        }
    }
}
