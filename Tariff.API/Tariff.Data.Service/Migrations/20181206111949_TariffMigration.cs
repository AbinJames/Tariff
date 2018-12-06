using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tariff.Data.Service.Migrations
{
    public partial class TariffMigration : Migration
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
                    table.ForeignKey(
                        name: "FK_RuleDetails_ParameterMaster_parameterId",
                        column: x => x.parameterId,
                        principalTable: "ParameterMaster",
                        principalColumn: "parameterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "InvoiceMaster",
                columns: new[] { "invoiceId", "invoiceName", "isActive" },
                values: new object[,]
                {
                    { 1, "Sample Invoice 1", (byte)1 },
                    { 2, "Sample invoice 2", (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "ParameterMaster",
                columns: new[] { "parameterId", "isActive", "parameterName" },
                values: new object[,]
                {
                    { 1, (byte)1, "Vessel Capacity" },
                    { 2, (byte)1, "Vessel Type" },
                    { 3, (byte)1, "LOA" }
                });

            migrationBuilder.InsertData(
                table: "RuleDetails",
                columns: new[] { "ruleId", "invoiceId", "isActive", "parameterId", "ruleValue" },
                values: new object[,]
                {
                    { 1, 1, (byte)1, 1, "Sample Rule 1" },
                    { 4, 2, (byte)1, 1, "Sample Rule 4" },
                    { 2, 1, (byte)1, 2, "Sample Rule 2" },
                    { 5, 2, (byte)1, 2, "Sample Rule 5" },
                    { 3, 1, (byte)1, 3, "Sample Rule 3" },
                    { 6, 2, (byte)1, 3, "Sample Rule 6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RuleDetails_invoiceId",
                table: "RuleDetails",
                column: "invoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RuleDetails_parameterId",
                table: "RuleDetails",
                column: "parameterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RuleDetails");

            migrationBuilder.DropTable(
                name: "InvoiceMaster");

            migrationBuilder.DropTable(
                name: "ParameterMaster");
        }
    }
}
