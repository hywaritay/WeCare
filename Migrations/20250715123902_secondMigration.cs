using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeCare.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("005d2e0e-d481-43b0-9124-5d8b4b49ed4e"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("15c7e901-c570-417e-8d41-41b9f0b48c89"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("2a695ba5-696e-4ea7-9d1d-39c4a3ed122b"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("599babcf-c59f-4917-9613-78be9cc803fe"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("656b8944-1110-41c7-bb00-196a49ccb910"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("76c7322e-ad36-4685-b9f7-61e1916e668b"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("7ab1aa9d-f6d5-400f-b4ce-8eb98dbba9d6"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("acae0e76-7c9d-4da1-9217-9c306152e665"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("f715eebe-4605-488c-aeb5-cac70662055b"));

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpExpiresAt",
                table: "TwoFactorAuths",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtpHash",
                table: "TwoFactorAuths",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "AgeInMonths", "AgeInWeeks", "Contraindications", "CreatedAt", "Description", "IsActive", "IsRequired", "Manufacturer", "Name", "SideEffects", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("01aac9db-24ab-4358-bad5-f057b1440f7d"), 1, 6, null, new DateTime(2025, 7, 15, 12, 39, 1, 488, DateTimeKind.Utc).AddTicks(4420), "Pneumococcal Conjugate Vaccine", true, true, "Various", "PCV", null, null },
                    { new Guid("2a1ddbf3-ae4a-44d6-b4bb-1326fc68b154"), 1, 6, null, new DateTime(2025, 7, 15, 12, 39, 1, 488, DateTimeKind.Utc).AddTicks(4416), "Haemophilus influenzae type b vaccine", true, true, "Various", "Hib", null, null },
                    { new Guid("51059893-6d5b-46e3-b2fe-fea84ae1a239"), 1, 6, null, new DateTime(2025, 7, 15, 12, 39, 1, 488, DateTimeKind.Utc).AddTicks(4410), "Oral Polio Vaccine", true, true, "Various", "OPV", null, null },
                    { new Guid("6051c475-0888-48d7-b26e-b17e5e44f473"), 1, 6, null, new DateTime(2025, 7, 15, 12, 39, 1, 488, DateTimeKind.Utc).AddTicks(4413), "Diphtheria, Pertussis, Tetanus vaccine", true, true, "Various", "DPT", null, null },
                    { new Guid("6f5acd55-3903-432c-9a67-eef6d6e3dffe"), 3, 14, null, new DateTime(2025, 7, 15, 12, 39, 1, 488, DateTimeKind.Utc).AddTicks(4426), "Inactivated Polio Vaccine", true, true, "Various", "IPV", null, null },
                    { new Guid("c2b7d0c6-a967-4e06-9b54-90e962926c84"), 0, 0, null, new DateTime(2025, 7, 15, 12, 39, 1, 488, DateTimeKind.Utc).AddTicks(4407), "Hepatitis B vaccine", true, true, "Various", "Hepatitis B", null, null },
                    { new Guid("d37fcbd0-e953-4349-af21-42640b9be904"), 0, 0, null, new DateTime(2025, 7, 15, 12, 39, 1, 488, DateTimeKind.Utc).AddTicks(4401), "Bacillus Calmette-Guérin vaccine for tuberculosis", true, true, "Various", "BCG", null, null },
                    { new Guid("dbd85ab0-1a0c-47da-baa9-5f818fabaf64"), 12, 52, null, new DateTime(2025, 7, 15, 12, 39, 1, 488, DateTimeKind.Utc).AddTicks(4432), "Measles, Mumps, Rubella vaccine", true, true, "Various", "MMR", null, null },
                    { new Guid("fdf829c6-96f8-4d66-8654-8df9c8247465"), 1, 6, null, new DateTime(2025, 7, 15, 12, 39, 1, 488, DateTimeKind.Utc).AddTicks(4423), "Rotavirus vaccine", true, true, "Various", "Rotavirus", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("01aac9db-24ab-4358-bad5-f057b1440f7d"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("2a1ddbf3-ae4a-44d6-b4bb-1326fc68b154"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("51059893-6d5b-46e3-b2fe-fea84ae1a239"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("6051c475-0888-48d7-b26e-b17e5e44f473"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("6f5acd55-3903-432c-9a67-eef6d6e3dffe"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("c2b7d0c6-a967-4e06-9b54-90e962926c84"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("d37fcbd0-e953-4349-af21-42640b9be904"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("dbd85ab0-1a0c-47da-baa9-5f818fabaf64"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("fdf829c6-96f8-4d66-8654-8df9c8247465"));

            migrationBuilder.DropColumn(
                name: "OtpExpiresAt",
                table: "TwoFactorAuths");

            migrationBuilder.DropColumn(
                name: "OtpHash",
                table: "TwoFactorAuths");

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "AgeInMonths", "AgeInWeeks", "Contraindications", "CreatedAt", "Description", "IsActive", "IsRequired", "Manufacturer", "Name", "SideEffects", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("005d2e0e-d481-43b0-9124-5d8b4b49ed4e"), 1, 6, null, new DateTime(2025, 7, 15, 11, 35, 26, 801, DateTimeKind.Utc).AddTicks(1004), "Pneumococcal Conjugate Vaccine", true, true, "Various", "PCV", null, null },
                    { new Guid("15c7e901-c570-417e-8d41-41b9f0b48c89"), 0, 0, null, new DateTime(2025, 7, 15, 11, 35, 26, 801, DateTimeKind.Utc).AddTicks(950), "Bacillus Calmette-Guérin vaccine for tuberculosis", true, true, "Various", "BCG", null, null },
                    { new Guid("2a695ba5-696e-4ea7-9d1d-39c4a3ed122b"), 1, 6, null, new DateTime(2025, 7, 15, 11, 35, 26, 801, DateTimeKind.Utc).AddTicks(1009), "Rotavirus vaccine", true, true, "Various", "Rotavirus", null, null },
                    { new Guid("599babcf-c59f-4917-9613-78be9cc803fe"), 1, 6, null, new DateTime(2025, 7, 15, 11, 35, 26, 801, DateTimeKind.Utc).AddTicks(989), "Diphtheria, Pertussis, Tetanus vaccine", true, true, "Various", "DPT", null, null },
                    { new Guid("656b8944-1110-41c7-bb00-196a49ccb910"), 0, 0, null, new DateTime(2025, 7, 15, 11, 35, 26, 801, DateTimeKind.Utc).AddTicks(977), "Hepatitis B vaccine", true, true, "Various", "Hepatitis B", null, null },
                    { new Guid("76c7322e-ad36-4685-b9f7-61e1916e668b"), 1, 6, null, new DateTime(2025, 7, 15, 11, 35, 26, 801, DateTimeKind.Utc).AddTicks(994), "Haemophilus influenzae type b vaccine", true, true, "Various", "Hib", null, null },
                    { new Guid("7ab1aa9d-f6d5-400f-b4ce-8eb98dbba9d6"), 12, 52, null, new DateTime(2025, 7, 15, 11, 35, 26, 801, DateTimeKind.Utc).AddTicks(1020), "Measles, Mumps, Rubella vaccine", true, true, "Various", "MMR", null, null },
                    { new Guid("acae0e76-7c9d-4da1-9217-9c306152e665"), 3, 14, null, new DateTime(2025, 7, 15, 11, 35, 26, 801, DateTimeKind.Utc).AddTicks(1014), "Inactivated Polio Vaccine", true, true, "Various", "IPV", null, null },
                    { new Guid("f715eebe-4605-488c-aeb5-cac70662055b"), 1, 6, null, new DateTime(2025, 7, 15, 11, 35, 26, 801, DateTimeKind.Utc).AddTicks(984), "Oral Polio Vaccine", true, true, "Various", "OPV", null, null }
                });
        }
    }
}
