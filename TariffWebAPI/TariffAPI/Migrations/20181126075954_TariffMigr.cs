using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TariffAPI.Migrations
{
    public partial class TariffMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceMaster",
                columns: table => new
                {
                    invoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    invoiceName = table.Column<string>(maxLength: 50, nullable: false),
                    isActive = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceMaster", x => x.invoiceId);
                });

            migrationBuilder.CreateTable(
                name: "ParameterMaster",
                columns: table => new
                {
                    parameterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    parameterName = table.Column<string>(maxLength: 50, nullable: false),
                    isActive = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterMaster", x => x.parameterId);
                });

            migrationBuilder.CreateTable(
                name: "RuleDetails",
                columns: table => new
                {
                    ruleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    invoiceId = table.Column<int>(nullable: false),
                    parameterId = table.Column<int>(nullable: false),
                    ruleValue = table.Column<string>(maxLength: 50, nullable: false),
                    isActive = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleDetails", x => x.ruleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceMaster");

            migrationBuilder.DropTable(
                name: "ParameterMaster");

            migrationBuilder.DropTable(
                name: "RuleDetails");
        }
    }
}
