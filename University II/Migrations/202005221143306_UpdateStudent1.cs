namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudent1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Students SET isEnrolled = '1' WHERE ID ='1'");
        }

        public override void Down()
        {
        }
    }
}
