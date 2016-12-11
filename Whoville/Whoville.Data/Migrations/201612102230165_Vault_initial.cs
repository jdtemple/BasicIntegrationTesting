namespace Whoville.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vault_initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Characters", "StoryId", "dbo.Stories");
            DropIndex("dbo.Characters", new[] { "StoryId" });
            CreateTable(
                "dbo.Cabinets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Folders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CabinetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cabinets", t => t.CabinetId, cascadeDelete: true)
                .Index(t => t.CabinetId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FolderId = c.Int(nullable: false),
                        Extension = c.String(),
                        DeletedDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Pages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Folders", t => t.FolderId, cascadeDelete: true)
                .Index(t => t.FolderId);
            
            DropTable("dbo.Characters");
            DropTable("dbo.Stories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Files", "FolderId", "dbo.Folders");
            DropForeignKey("dbo.Folders", "CabinetId", "dbo.Cabinets");
            DropIndex("dbo.Files", new[] { "FolderId" });
            DropIndex("dbo.Folders", new[] { "CabinetId" });
            DropTable("dbo.Files");
            DropTable("dbo.Folders");
            DropTable("dbo.Cabinets");
            CreateIndex("dbo.Characters", "StoryId");
            AddForeignKey("dbo.Characters", "StoryId", "dbo.Stories", "Id", cascadeDelete: true);
        }
    }
}
