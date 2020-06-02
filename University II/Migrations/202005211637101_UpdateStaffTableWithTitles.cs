namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStaffTableWithTitles : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE StaffMembers SET Title = 'Dean' WHERE StaffMemberID = '1'");

        }

        public override void Down()
        {
        }
    }
}
