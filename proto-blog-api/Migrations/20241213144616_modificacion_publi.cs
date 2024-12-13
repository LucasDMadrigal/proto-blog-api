using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proto_blog_api.Migrations
{
    /// <inheritdoc />
    public partial class modificacion_publi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Publications_PublicationsId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_PublicationsId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "PublicationsId",
                table: "Authors");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Publications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Publications");

            migrationBuilder.AddColumn<int>(
                name: "PublicationsId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_PublicationsId",
                table: "Authors",
                column: "PublicationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Publications_PublicationsId",
                table: "Authors",
                column: "PublicationsId",
                principalTable: "Publications",
                principalColumn: "Id");
        }
    }
}
