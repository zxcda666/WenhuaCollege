namespace WenHuaCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixIdentityChange : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Personnel");
            AlterColumn("dbo.Personnel", "PersonnelID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Personnel", "PersonnelID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Personnel");
            AlterColumn("dbo.Personnel", "PersonnelID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Personnel", "PersonnelID");
        }
    }
}
