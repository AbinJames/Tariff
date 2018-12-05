using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TariffAPI.Migrations
{
    public partial class TarifFMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceMaster",
                columns: table => new
                {
                    invoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    invoiceName = table.Column<string>(nullable: false),
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
                    parameterName = table.Column<string>(nullable: false),
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
                    ruleValue = table.Column<string>(nullable: false),
                    isActive = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleDetails", x => x.ruleId);
                    table.ForeignKey(
                        name: "FK_RuleDetails_InvoiceMaster_invoiceId",
                        column: x => x.invoiceId,
                        principalTable: "InvoiceMaster",
                        principalColumn: "invoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RuleDetails_invoiceId",
                table: "RuleDetails",
                column: "invoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParameterMaster");

            migrationBuilder.DropTable(
                name: "RuleDetails");

            migrationBuilder.DropTable(
                name: "InvoiceMaster");
        }
    }
}
