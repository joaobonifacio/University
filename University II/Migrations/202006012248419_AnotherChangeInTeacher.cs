namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotherChangeInTeacher : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "Salary", c => c.Int(nullable: false));
        }

        public override void Down()
        {
        }
    }
}
