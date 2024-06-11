using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab11.Migrations
{
    /// <inheritdoc />
    public partial class Test001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deatails",
                table: "PrescriptionMedicaments",
                newName: "Details");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "PrescriptionMedicaments",
                newName: "Deatails");
        }
    }
}
