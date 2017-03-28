namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "project_id", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "project_id" });
            RenameColumn(table: "dbo.Projects", name: "Team_id", newName: "TeamId");
            RenameIndex(table: "dbo.Projects", name: "IX_Team_id", newName: "IX_TeamId");
            AddColumn("dbo.Tasks", "assigned", c => c.String());
            AddColumn("dbo.Teams", "LeaderId", c => c.String());
            AlterColumn("dbo.Tasks", "Project_id", c => c.Int());
            CreateIndex("dbo.Tasks", "Project_id");
            AddForeignKey("dbo.Tasks", "Project_id", "dbo.Projects", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Project_id", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "Project_id" });
            AlterColumn("dbo.Tasks", "Project_id", c => c.Int(nullable: false));
            DropColumn("dbo.Teams", "LeaderId");
            DropColumn("dbo.Tasks", "assigned");
            RenameIndex(table: "dbo.Projects", name: "IX_TeamId", newName: "IX_Team_id");
            RenameColumn(table: "dbo.Projects", name: "TeamId", newName: "Team_id");
            CreateIndex("dbo.Tasks", "project_id");
            AddForeignKey("dbo.Tasks", "project_id", "dbo.Projects", "id", cascadeDelete: true);
        }
    }
}
