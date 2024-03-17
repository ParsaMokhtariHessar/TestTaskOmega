using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTaskOmega.Identity.Migrations
{
    /// <inheritdoc />
    public partial class MYmigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c8512c9-6130-45ca-b3f7-0e97be4cd46c", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEIrJ+f/wxgvH4iZlHO5WeoZHeHwrtAbIYBTNoGOjbLHOY5oBqcEkVzMGdacQTlonLQ==", "157de6c1-a855-4f7c-802c-94f1083cf2e3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23a2600f-ba65-4be0-921e-8537b163efcc", "ADMIN@admin.COM", "$2a$11$/c5N38i/gpJPAwHxuGUnPONnWfAgoogVfWXWqAlZ9DqeHZkbCpsXG", "bc7f0b40-9bf4-4fd6-b257-a9ce5b441e24" });
        }
    }
}
