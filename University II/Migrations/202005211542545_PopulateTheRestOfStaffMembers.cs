namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTheRestOfStaffMembers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO StaffMembers (Email) VALUES ('secretary@uni.com')");

            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher1@gmail.com')");
            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher2@gmail.com')");
            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher3@gmail.com')");
            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher4@gmail.com')");
            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher5@gmail.com')");
            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher6@gmail.com')");
            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher7@gmail.com')");
            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher8@gmail.com')");
            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher9@gmail.com')");
            Sql("INSERT INTO StaffMembers (Email) VALUES ('teacher10@gmail.com')");


        }

        public override void Down()
        {
        }
    }
}
