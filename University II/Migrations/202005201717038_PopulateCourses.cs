namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCourses : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Courses (Title) VALUES ('Electronics Engineering')");

            Sql("INSERT INTO Courses (Title) VALUES ('Physics Engineering')");

        }

        public override void Down()
        {
        }
    }
}
