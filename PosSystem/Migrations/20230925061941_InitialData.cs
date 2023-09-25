using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName", "CreatedAt", "CreatedBy", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Description", "Gà Rán", new DateTime(2023, 9, 25, 13, 19, 41, 845, DateTimeKind.Local).AddTicks(2988), "Admin", true, new DateTime(2023, 9, 25, 13, 19, 41, 845, DateTimeKind.Local).AddTicks(3010), "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
