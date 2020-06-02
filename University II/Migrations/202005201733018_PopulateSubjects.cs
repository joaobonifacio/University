namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSubjects : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Calculus I', 3)");
        }
        
        public override void Down()
        {
        }
    }
}
