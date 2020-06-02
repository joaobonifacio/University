namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequiredToSubjectModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subjects", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "Title", c => c.String());
        }
    }
}
