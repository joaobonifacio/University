namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInSubjectModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "TeacherId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "TeacherId");
        }
    }
}
