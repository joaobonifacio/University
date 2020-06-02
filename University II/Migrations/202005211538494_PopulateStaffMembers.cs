namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStaffMembers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO StaffMembers (Email) VALUES ('dean@uni.com')");

        }

        public override void Down()
        {
        }
    }
}
