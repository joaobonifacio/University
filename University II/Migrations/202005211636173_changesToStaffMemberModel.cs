namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesToStaffMemberModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffMembers", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffMembers", "Title");
        }
    }
}
