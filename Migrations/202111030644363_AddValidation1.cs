namespace EmployeeInformationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidation1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "CellPhone", c => c.String(nullable: false, maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "CellPhone", c => c.String());
        }
    }
}
