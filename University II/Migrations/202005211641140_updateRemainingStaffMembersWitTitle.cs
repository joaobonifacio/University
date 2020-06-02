namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRemainingStaffMembersWitTitle : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE StaffMembers SET Title = 'Secretary' WHERE StaffMemberID = '2'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '3'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '4'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '5'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '6'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '7'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '8'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '9'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '10'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '11'");
            Sql("UPDATE StaffMembers SET Title = 'Teacher' WHERE StaffMemberID = '12'");
           

        }

        public override void Down()
        {
        }
    }
}
