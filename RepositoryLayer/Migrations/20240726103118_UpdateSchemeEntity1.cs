using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class UpdateSchemeEntity1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSchemes_Schemes_SchemeID",
                table: "EmployeeSchemes");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSchemes_Schemes_SchemeID",
                table: "EmployeeSchemes",
                column: "SchemeID",
                principalTable: "Schemes",
                principalColumn: "SchemeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSchemes_Schemes_SchemeID",
                table: "EmployeeSchemes");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSchemes_Schemes_SchemeID",
                table: "EmployeeSchemes",
                column: "SchemeID",
                principalTable: "Schemes",
                principalColumn: "SchemeID");
        }
    }
}
