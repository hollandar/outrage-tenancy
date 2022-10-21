using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tenancy.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class tenantIv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Iv",
                table: "Tenants",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iv",
                table: "Tenants");
        }
    }
}
