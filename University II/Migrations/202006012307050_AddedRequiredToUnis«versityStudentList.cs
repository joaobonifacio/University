namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequiredToUnisversityStudentList : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UniversityStudentsLists", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.UniversityStudentsLists", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UniversityStudentsLists", "Email", c => c.String());
            AlterColumn("dbo.UniversityStudentsLists", "Name", c => c.String());
        }
    }
}
