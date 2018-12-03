namespace HockeyPlayerDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "ClubId", "dbo.Clubs");
            DropIndex("dbo.Players", new[] { "ClubId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Players", "ClubId");
            AddForeignKey("dbo.Players", "ClubId", "dbo.Clubs", "Id");
        }
    }
}
