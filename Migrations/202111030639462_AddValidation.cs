namespace EmployeeInformationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ConfirmEmail", c => c.String());
            AlterColumn("dbo.Employees", "EmployeeName", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Email", c => c.String());
            AlterColumn("dbo.Employees", "EmployeeName", c => c.String());
            DropColumn("dbo.Employees", "ConfirmEmail");
        }
    }
}
