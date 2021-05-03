using Microsoft.EntityFrameworkCore.Migrations;

namespace Student.Persistence.Migrations
{
    public partial class ChangeGroupStudentIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudent_Students_GroupsId1",
                table: "GroupStudent");

            migrationBuilder.RenameColumn(
                name: "GroupsId1",
                table: "GroupStudent",
                newName: "StudentsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupStudent_GroupsId1",
                table: "GroupStudent",
                newName: "IX_GroupStudent_StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudent_Students_StudentsId",
                table: "GroupStudent",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudent_Students_StudentsId",
                table: "GroupStudent");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "GroupStudent",
                newName: "GroupsId1");

            migrationBuilder.RenameIndex(
                name: "IX_GroupStudent_StudentsId",
                table: "GroupStudent",
                newName: "IX_GroupStudent_GroupsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudent_Students_GroupsId1",
                table: "GroupStudent",
                column: "GroupsId1",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
