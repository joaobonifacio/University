namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToTeacherModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "Salary", c => c.String(nullable: false));
        }

        public override void Down()
        {
        }
    }
}
