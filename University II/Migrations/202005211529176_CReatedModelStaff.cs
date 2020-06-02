namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CReatedModelStaff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StaffMembers",
                c => new
                    {
                        StaffMemberID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.StaffMemberID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StaffMembers");
        }
    }
}
