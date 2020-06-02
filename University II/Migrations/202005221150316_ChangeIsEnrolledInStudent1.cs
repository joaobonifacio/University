namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIsEnrolledInStudent1 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Students (isEnrolled) VALUES (1)");
        }

        public override void Down()
        {
        }
    }
}
