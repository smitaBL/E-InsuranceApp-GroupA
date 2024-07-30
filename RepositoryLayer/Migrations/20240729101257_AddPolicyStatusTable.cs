using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class AddPolicyStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PolicyStatus",
                columns: table => new
                {
                    PolicyStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyStatus", x => x.PolicyStatusID);
                    table.ForeignKey(
                        name: "FK_PolicyStatus_Policies_PolicyID",
                        column: x => x.PolicyID,
                        principalTable: "Policies",
                        principalColumn: "PolicyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchemeWithInsurancePlanML",
                columns: table => new
                {
                    SchemeID = table.Column<int>(type: "int", nullable: false),
                    SchemeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchemeDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchemePrice = table.Column<double>(type: "float", nullable: false),
                    SchemeCover = table.Column<double>(type: "float", nullable: false),
                    SchemeTenure = table.Column<int>(type: "int", nullable: false),
                    PlanID = table.Column<int>(type: "int", nullable: false),
                    SchemeCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolicyStatus_PolicyID",
                table: "PolicyStatus",
                column: "PolicyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyStatus");

            migrationBuilder.DropTable(
                name: "SchemeWithInsurancePlanML");
        }
    }
}
