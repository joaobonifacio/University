namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTeachersInSubjectsTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Subjects SET TeacherId = 2 WHERE ID = '2'");

            Sql("UPDATE Subjects SET TeacherId = 3 WHERE ID = '3'");

            Sql("UPDATE Subjects SET TeacherId = 4 WHERE ID = '4'");

            Sql("UPDATE Subjects SET TeacherId = 5 WHERE ID = '5'");

            Sql("UPDATE Subjects SET TeacherId = 6 WHERE ID = '6'");

            Sql("UPDATE Subjects SET TeacherId = 7 WHERE ID = '7'");

            Sql("UPDATE Subjects SET TeacherId = 8 WHERE ID = '8'");

            Sql("UPDATE Subjects SET TeacherId = 9 WHERE ID = '9'");

            Sql("UPDATE Subjects SET TeacherId = 10 WHERE ID = '10'");

        }

        public override void Down()
        {
        }
    }
}
