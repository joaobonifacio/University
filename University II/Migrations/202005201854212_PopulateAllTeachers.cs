namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAllTeachers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('Bill Gates', 2000)");

            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('Diego Maradona', 2000)");

            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('Sobrinho Simões', 2000)");

            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('João Torgal', 2000)");

            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('Mick Jaggers', 2000)");

            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('Stanley Kubrick', 2000)");

            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('António Lobo Antunes', 2000)");

            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('James Brown', 2000)");

            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('Michelle Mutton', 2000)");


        }

        public override void Down()
        {
        }
    }
}
