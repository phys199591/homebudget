namespace EntityFrameworkCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseRowVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expense", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expense", "RowVersion");
        }
    }
}
