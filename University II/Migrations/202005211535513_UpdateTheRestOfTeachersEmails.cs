namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTheRestOfTeachersEmails : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Teachers SET Email = 'teacher2@gmail.com' WHERE ID = '2'");
            
            Sql("UPDATE Teachers SET Email = 'teacher3@gmail.com' WHERE ID = '3'");

            Sql("UPDATE Teachers SET Email = 'teacher4@gmail.com' WHERE ID = '4'");

            Sql("UPDATE Teachers SET Email = 'teacher5@gmail.com' WHERE ID = '5'");
            
            Sql("UPDATE Teachers SET Email = 'teacher6@gmail.com' WHERE ID = '6'");
            
            Sql("UPDATE Teachers SET Email = 'teacher7@gmail.com' WHERE ID = '7'");
            
            Sql("UPDATE Teachers SET Email = 'teacher8@gmail.com' WHERE ID = '8'");
            
            Sql("UPDATE Teachers SET Email = 'teacher9@gmail.com' WHERE ID = '9'");
            
            Sql("UPDATE Teachers SET Email = 'teacher10@gmail.com' WHERE ID = '10'");

        }

        public override void Down()
        {
        }
    }
}
