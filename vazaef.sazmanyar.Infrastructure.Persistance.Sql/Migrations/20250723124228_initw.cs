using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Migrations
{
    /// <inheritdoc />
    public partial class initw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_Allocations_AllocationId",
                table: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_AllocationId",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "AllocationId",
                table: "PaymentMethods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AllocationId",
                table: "PaymentMethods",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_AllocationId",
                table: "PaymentMethods",
                column: "AllocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Allocations_AllocationId",
                table: "PaymentMethods",
                column: "AllocationId",
                principalTable: "Allocations",
                principalColumn: "Id");
        }
    }
}
