using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class Added_EmployeeScheme_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEntitySchemeEntity");

            migrationBuilder.CreateTable(
                name: "EmployeeSchemes",
                columns: table => new
                {
                    EmployeeSchemeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    SchemeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSchemes", x => x.EmployeeSchemeID);
                    table.ForeignKey(
                        name: "FK_EmployeeSchemes_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_EmployeeSchemes_Schemes_SchemeID",
                        column: x => x.SchemeID,
                        principalTable: "Schemes",
                        principalColumn: "SchemeID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchemes_EmployeeID",
                table: "EmployeeSchemes",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchemes_SchemeID",
                table: "EmployeeSchemes",
                column: "SchemeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSchemes");

            migrationBuilder.CreateTable(
                name: "EmployeeEntitySchemeEntity",
                columns: table => new
                {
                    EmployeesEmployeeID = table.Column<int>(type: "int", nullable: false),
                    SchemesSchemeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEntitySchemeEntity", x => new { x.EmployeesEmployeeID, x.SchemesSchemeID });
                    table.ForeignKey(
                        name: "FK_EmployeeEntitySchemeEntity_Employees_EmployeesEmployeeID",
                        column: x => x.EmployeesEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeEntitySchemeEntity_Schemes_SchemesSchemeID",
                        column: x => x.SchemesSchemeID,
                        principalTable: "Schemes",
                        principalColumn: "SchemeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEntitySchemeEntity_SchemesSchemeID",
                table: "EmployeeEntitySchemeEntity",
                column: "SchemesSchemeID");
        }
    }
}
