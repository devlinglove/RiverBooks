using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiverBooks.Users.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserStreetAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress_Street2",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "Street2");

            migrationBuilder.RenameColumn(
                name: "StreetAddress_Street1",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "Street1");

            migrationBuilder.RenameColumn(
                name: "StreetAddress_State",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "StreetAddress_PostalCode",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "Postal Code");

            migrationBuilder.RenameColumn(
                name: "StreetAddress_Country",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "StreetAddress_City",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street2",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "StreetAddress_Street2");

            migrationBuilder.RenameColumn(
                name: "Street1",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "StreetAddress_Street1");

            migrationBuilder.RenameColumn(
                name: "State",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "StreetAddress_State");

            migrationBuilder.RenameColumn(
                name: "Postal Code",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "StreetAddress_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "StreetAddress_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                schema: "Users",
                table: "UserStreetAddress",
                newName: "StreetAddress_City");
        }
    }
}
