using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.API.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInboxs",
                table: "OrderInboxs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderInboxs");

            migrationBuilder.AddColumn<Guid>(
                name: "IdempotentToken",
                table: "OrderInboxs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInboxs",
                table: "OrderInboxs",
                column: "IdempotentToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInboxs",
                table: "OrderInboxs");

            migrationBuilder.DropColumn(
                name: "IdempotentToken",
                table: "OrderInboxs");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderInboxs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInboxs",
                table: "OrderInboxs",
                column: "Id");
        }
    }
}
