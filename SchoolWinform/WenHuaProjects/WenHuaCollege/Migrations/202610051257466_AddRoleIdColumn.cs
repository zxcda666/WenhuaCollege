namespace WenHuaCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleIdColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personnel", "RoleID", c => c.Int(nullable: false, defaultValue: 1));
        }

        public override void Down()
        {
            DropColumn("dbo.Personnel", "RoleID");
        }
    }
}
