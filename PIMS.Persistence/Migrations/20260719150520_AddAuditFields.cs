using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Users",
                newName: "LastModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Users",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Roles",
                newName: "LastModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Roles",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Products",
                newName: "LastModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Products",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "InventoryTransactions",
                newName: "LastModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "InventoryTransactions",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransactions_CreatedOn",
                table: "InventoryTransactions",
                newName: "IX_InventoryTransactions_CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Inventories",
                newName: "LastModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Inventories",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Categories",
                newName: "LastModifiedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Categories",
                newName: "CreatedOnUtc");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "CreatedBy",
                keyValue: null,
                column: "CreatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Users",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Users",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Users",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "CreatedBy",
                keyValue: null,
                column: "CreatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Roles",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Roles",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Roles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Roles",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "CreatedBy",
                keyValue: null,
                column: "CreatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Products",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Products",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Products",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Products",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "InventoryTransactions",
                keyColumn: "CreatedBy",
                keyValue: null,
                column: "CreatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "InventoryTransactions",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "InventoryTransactions",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "InventoryTransactions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InventoryTransactions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "InventoryTransactions",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "CreatedBy",
                keyValue: null,
                column: "CreatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Inventories",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Inventories",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Inventories",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Inventories",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Inventories",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CreatedBy",
                keyValue: null,
                column: "CreatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Categories",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Categories",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Categories",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "LastModifiedOnUtc",
                table: "Users",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Users",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "LastModifiedOnUtc",
                table: "Roles",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Roles",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "LastModifiedOnUtc",
                table: "Products",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Products",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "LastModifiedOnUtc",
                table: "InventoryTransactions",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "InventoryTransactions",
                newName: "CreatedOn");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransactions_CreatedOnUtc",
                table: "InventoryTransactions",
                newName: "IX_InventoryTransactions_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "LastModifiedOnUtc",
                table: "Inventories",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Inventories",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "LastModifiedOnUtc",
                table: "Categories",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Categories",
                newName: "CreatedOn");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Roles",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "InventoryTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "InventoryTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Inventories",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Inventories",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Categories",
                type: "int",
                nullable: true);
        }
    }
}
