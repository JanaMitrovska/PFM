using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PFMApi.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CategoryCode",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MccCodeCode",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSplited",
                table: "Transactions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MccCodes",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MerchanTtype = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MccCodes", x => x.Code);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MccCodeCode",
                table: "Transactions",
                column: "MccCodeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_MccCodes_MccCodeCode",
                table: "Transactions",
                column: "MccCodeCode",
                principalTable: "MccCodes",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_MccCodes_MccCodeCode",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "MccCodes");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_MccCodeCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CategoryCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "MccCodeCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "isSplited",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Transactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
