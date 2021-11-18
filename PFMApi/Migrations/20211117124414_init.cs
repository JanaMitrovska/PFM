using Microsoft.EntityFrameworkCore.Migrations;

namespace PFMApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BenificaryName = table.Column<string>(maxLength: 255, nullable: true),
                    Date = table.Column<string>(nullable: false),
                    Direction = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Currency = table.Column<string>(maxLength: 3, nullable: false),
                    Mcc = table.Column<int>(nullable: true),
                    Kind = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
