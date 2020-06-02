namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudent : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Students SET isEnrolled = true WHERE ID='1'");
        }
        
        public override void Down()
        {
        }
    }
}
