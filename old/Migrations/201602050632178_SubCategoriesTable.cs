namespace EntityFrameworkCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCategoriesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseSubCategory",
                c => new
                    {
                        ExpenseSubCategoryId = c.Int(nullable: false, identity: true),
                        ExpenseId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ExpenseSubCategoryId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: false)
                .ForeignKey("dbo.Expense", t => t.ExpenseId, cascadeDelete: false)
                .Index(t => t.ExpenseId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: false)
                .Index(t => t.CategoryId);
            
            AlterColumn("dbo.Category", "Name", c => c.String(maxLength: 20));
            AlterColumn("dbo.Expense", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.ExpenseSubCategory", "ExpenseId", "dbo.Expense");
            DropForeignKey("dbo.ExpenseSubCategory", "CategoryId", "dbo.Category");
            DropIndex("dbo.SubCategory", new[] { "CategoryId" });
            DropIndex("dbo.ExpenseSubCategory", new[] { "CategoryId" });
            DropIndex("dbo.ExpenseSubCategory", new[] { "ExpenseId" });
            AlterColumn("dbo.Expense", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Category", "Name", c => c.String());
            DropTable("dbo.SubCategory");
            DropTable("dbo.ExpenseSubCategory");
        }
    }
}
