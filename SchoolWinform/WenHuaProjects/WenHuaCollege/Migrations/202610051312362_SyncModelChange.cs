namespace WenHuaCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncModelChange : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.Personnel", "menuID");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Personnel", "menuID", c => c.String());
        }
    }
}
