namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Project_id", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "Project_id" });
            RenameColumn(table: "dbo.Projects", name: "TeamId", newName: "Team_id");
            RenameIndex(table: "dbo.Projects", name: "IX_TeamId", newName: "IX_Team_id");
            AlterColumn("dbo.Tasks", "project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "project_id");
            AddForeignKey("dbo.Tasks", "project_id", "dbo.Projects", "id", cascadeDelete: true);
            DropColumn("dbo.Tasks", "assigned");
            DropColumn("dbo.Teams", "LeaderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "LeaderId", c => c.String());
            AddColumn("dbo.Tasks", "assigned", c => c.String());
            DropForeignKey("dbo.Tasks", "project_id", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "project_id" });
            AlterColumn("dbo.Tasks", "project_id", c => c.Int());
            RenameIndex(table: "dbo.Projects", name: "IX_Team_id", newName: "IX_TeamId");
            RenameColumn(table: "dbo.Projects", name: "Team_id", newName: "TeamId");
            CreateIndex("dbo.Tasks", "Project_id");
            AddForeignKey("dbo.Tasks", "Project_id", "dbo.Projects", "id");
        }
    }
}
