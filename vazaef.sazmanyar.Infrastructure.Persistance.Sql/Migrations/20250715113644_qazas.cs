using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Migrations
{
    /// <inheritdoc />
    public partial class qazas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "budgetEstimationRanges",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "year",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "budgetEstimationRanges",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "year",
                table: "Requests");
        }
    }
}
