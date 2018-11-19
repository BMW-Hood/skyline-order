using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderNO = table.Column<string>(maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    PaymentNO = table.Column<string>(maxLength: 50, nullable: false),
                    Channel = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PaymentAmount = table.Column<decimal>(nullable: false),
                    PayTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
