using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3efc10c3-7c29-4251-a0ad-3e8f11b9c629", null, "Administrator", "ADMINISTRATOR" },
                    { "401a0755-c4e1-4cd2-93c2-29f00cc1ebce", null, "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 10, 16, 10, 43, 4, 271, DateTimeKind.Local).AddTicks(2574), new DateTime(2023, 10, 16, 10, 43, 4, 271, DateTimeKind.Local).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 10, 16, 10, 43, 4, 271, DateTimeKind.Local).AddTicks(2745), new DateTime(2023, 10, 16, 10, 43, 4, 271, DateTimeKind.Local).AddTicks(2747) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 10, 16, 10, 43, 4, 271, DateTimeKind.Local).AddTicks(3204), new DateTime(2023, 10, 16, 10, 43, 4, 271, DateTimeKind.Local).AddTicks(3206) });

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "SupplierId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991855"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 10, 16, 10, 43, 4, 271, DateTimeKind.Local).AddTicks(3493), new DateTime(2023, 10, 16, 10, 43, 4, 271, DateTimeKind.Local).AddTicks(3495) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3efc10c3-7c29-4251-a0ad-3e8f11b9c629");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "401a0755-c4e1-4cd2-93c2-29f00cc1ebce");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 10, 15, 21, 24, 54, 679, DateTimeKind.Local).AddTicks(5282), new DateTime(2023, 10, 15, 21, 24, 54, 679, DateTimeKind.Local).AddTicks(5302) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 10, 15, 21, 24, 54, 679, DateTimeKind.Local).AddTicks(5307), new DateTime(2023, 10, 15, 21, 24, 54, 679, DateTimeKind.Local).AddTicks(5308) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 10, 15, 21, 24, 54, 679, DateTimeKind.Local).AddTicks(5719), new DateTime(2023, 10, 15, 21, 24, 54, 679, DateTimeKind.Local).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "SupplierId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991855"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 10, 15, 21, 24, 54, 679, DateTimeKind.Local).AddTicks(5918), new DateTime(2023, 10, 15, 21, 24, 54, 679, DateTimeKind.Local).AddTicks(5920) });
        }
    }
}
