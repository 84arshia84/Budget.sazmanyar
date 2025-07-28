using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Migrations
{
    /// <inheritdoc />
    public partial class initqPAq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Allocations_AllocationId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_FundingSources_FundingSourceId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestTypes_RequestTypeId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestingDepartments_RequestingDepartmentId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Allocations_AllocationId",
                table: "Payments",
                column: "AllocationId",
                principalTable: "Allocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_FundingSources_FundingSourceId",
                table: "Requests",
                column: "FundingSourceId",
                principalTable: "FundingSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestTypes_RequestTypeId",
                table: "Requests",
                column: "RequestTypeId",
                principalTable: "RequestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestingDepartments_RequestingDepartmentId",
                table: "Requests",
                column: "RequestingDepartmentId",
                principalTable: "RequestingDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Allocations_AllocationId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_FundingSources_FundingSourceId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestTypes_RequestTypeId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestingDepartments_RequestingDepartmentId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Allocations_AllocationId",
                table: "Payments",
                column: "AllocationId",
                principalTable: "Allocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_FundingSources_FundingSourceId",
                table: "Requests",
                column: "FundingSourceId",
                principalTable: "FundingSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestTypes_RequestTypeId",
                table: "Requests",
                column: "RequestTypeId",
                principalTable: "RequestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestingDepartments_RequestingDepartmentId",
                table: "Requests",
                column: "RequestingDepartmentId",
                principalTable: "RequestingDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
