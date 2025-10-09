using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityManagementApp_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EnrollmentDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Email", "EnrollmentDate", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "ahmed.hassan@university.edu", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmed", "Hassan", "0123456789" },
                    { 2, "fatima.ali@university.edu", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fatima", "Ali", "0123456788" },
                    { 3, "omar.mohamed@university.edu", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omar", "Mohamed", "0123456787" },
                    { 4, "yasmin.ibrahim@university.edu", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yasmin", "Ibrahim", "0123456786" },
                    { 5, "khalid.mahmoud@university.edu", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khalid", "Mahmoud", "0123456785" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
