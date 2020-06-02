namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequiredAnnotationToTeacherModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "Email", c => c.String(nullable: false));

        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teachers", "Email", c => c.String());
            AlterColumn("dbo.Teachers", "Name", c => c.String());
        }
    }
}
