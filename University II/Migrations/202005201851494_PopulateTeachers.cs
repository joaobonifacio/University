using System.Web.UI.WebControls;

namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTeachers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Teachers (Name, Salary) VALUES ('Woody Allen', 2000)");
        }
        
        public override void Down()
        {
        }
    }
}
