namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInStudentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "isEnrolled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "isEnrolled");
        }
    }
}
