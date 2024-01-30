using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondaryEmailRemoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondaryPhone",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "GOVID",
                table: "Users",
                newName: "GovId");

            migrationBuilder.RenameColumn(
                name: "FISCALID",
                table: "Users",
                newName: "FiscalId");

            migrationBuilder.RenameColumn(
                name: "CORPORATEID",
                table: "Users",
                newName: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GovId",
                table: "Users",
                newName: "GOVID");

            migrationBuilder.RenameColumn(
                name: "FiscalId",
                table: "Users",
                newName: "FISCALID");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Users",
                newName: "CORPORATEID");

            migrationBuilder.AddColumn<string>(
                name: "SecondaryPhone",
                table: "Users",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
