﻿namespace University_II.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesInAccountViewModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Email", c => c.String());
            AlterColumn("dbo.Students", "Name", c => c.String());
        }
    }
}
