using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PFMApi.Migrations
{
    public partial class new_relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryCode",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MccCodeCode",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSplited",
                table: "Transactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MccCodes",
                columns: table => new
                {
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MerchanTtype = table.Column<string>(type: "text", nullable: true)
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
    }
}
