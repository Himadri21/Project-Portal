namespace ProjectProgress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ne : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teacher", "TeacherEmail", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teacher", "TeacherEmail");
        }
    }
}
