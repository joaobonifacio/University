namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSubjectsWithTeachers : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Subjects SET TeacherId = 1 WHERE ID = '1'");
        }
        
        public override void Down()
        {
        }
    }
}
