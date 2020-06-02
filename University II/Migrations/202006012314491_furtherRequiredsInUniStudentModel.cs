namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class furtherRequiredsInUniStudentModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UniversityStudentsLists", "Birthday", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UniversityStudentsLists", "Birthday", c => c.DateTime());
        }
    }
}
