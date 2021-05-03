using Microsoft.EntityFrameworkCore.Migrations;

namespace Student.Persistence.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_FirstName",
                table: "Students",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Gender",
                table: "Students",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_Students_LastName",
                table: "Students",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MiddleName",
                table: "Students",
                column: "MiddleName");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                table: "Groups",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_FirstName",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Gender",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_LastName",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_MiddleName",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Name",
                table: "Groups");
        }
    }
}
