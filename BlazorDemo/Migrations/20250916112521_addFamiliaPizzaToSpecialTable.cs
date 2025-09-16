using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorDemo.Migrations
{
    /// <inheritdoc />
    public partial class addFamiliaPizzaToSpecialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FixedSize",
                table: "Specials",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedSize",
                table: "Specials");
        }
    }
}
