using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBinaryProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "resumes",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "job_seekers",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "resumes");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "job_seekers");
        }
    }
}
