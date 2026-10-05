namespace WenHuaCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonnel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personnel",
                c => new
                    {
                        PersonnelID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.PersonnelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Personnel");
        }
    }
}
