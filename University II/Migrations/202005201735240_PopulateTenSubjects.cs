namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTenSubjects : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Physics I', 3)");

            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Chemistry I', 3)");
            
            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Statistics', 3)");

            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Digital Systems', 3)");

            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Electromagnetism', 3)");

            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Chemistry Laboratorial Technics', 3)");

            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Programming I', 3)");

            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Organic Chemistry', 3)");

            Sql("INSERT INTO Subjects (Title, Credits) VALUES ('Quantum Physics', 3)");

        }

        public override void Down()
        {
        }
    }
}
