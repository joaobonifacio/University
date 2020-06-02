namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmailsInTeachers1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Teachers SET Email = 'teacher1@gmail.com' WHERE ID = '1'");

        }

        public override void Down()
        {
        }
    }
}
