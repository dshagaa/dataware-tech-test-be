using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveysApi.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(36)", maxLength: 36, nullable: false),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(36)", maxLength: 36, nullable: false),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(36)", maxLength: 36, nullable: false),
                    first_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    token = table.Column<string>(type: "text", nullable: true),
                    recovery_token = table.Column<string>(type: "text", nullable: true),
                    status_id = table.Column<string>(type: "char(36)", maxLength: 36, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<string>(type: "char(36)", nullable: false),
                    UsersId = table.Column<string>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "surveys",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(36)", maxLength: 36, nullable: false),
                    user_id = table.Column<string>(type: "char(36)", maxLength: 36, nullable: false),
                    title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    registration_start_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    registration_end_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    configuration = table.Column<object>(type: "json", nullable: true),
                    metadata = table.Column<object>(type: "json", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys", x => x.id);
                    table.ForeignKey(
                        name: "FK_surveys_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_surveys_user_id",
                table: "surveys",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_status_id",
                table: "users",
                column: "status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "surveys");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "statuses");
        }
    }
}
