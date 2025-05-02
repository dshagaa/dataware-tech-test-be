using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveysApi.Migrations
{
    /// <inheritdoc />
    public partial class FillDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO roles (id, code, description, created_at) VALUES ('a6c6df99-9aa3-460d-86fa-ccec92a22f93', 'user', 'User', now())");
            migrationBuilder.Sql("INSERT INTO roles (id, code, description, created_at) VALUES ('7a47a457-1b96-4240-8289-d95b9469e6f3', 'admin', 'Admin', now())");

            migrationBuilder.Sql("INSERT INTO statuses (id, code, description, created_at) VALUES ('bd964a3c-119c-4bb2-9fae-54e1a875476a', 'active', 'Active', now())");
            migrationBuilder.Sql("INSERT INTO statuses (id, code, description, created_at) VALUES ('3ff65207-ed37-499c-8aa1-c64929e75122', 'inactive', 'Inactive', now())");
            migrationBuilder.Sql("INSERT INTO statuses (id, code, description, created_at) VALUES ('6082b96c-f2ad-414b-a62f-aedfdf9c7dbd', 'banned', 'Banned', now())");
            migrationBuilder.Sql("INSERT INTO statuses (id, code, description, created_at) VALUES ('04ccec7e-bf06-4c44-a5ad-8e26719bb756', 'deleted', 'Deleted', now())");

            var userStatusId = "bd964a3c-119c-4bb2-9fae-54e1a875476a";
            var adminRoleId = "7a47a457-1b96-4240-8289-d95b9469e6f3";
            var userId = Guid.NewGuid().ToString();
            migrationBuilder.Sql("INSERT INTO users (id, first_name, last_name, username, password, token, status_id, role_id, created_at) VALUES ('"+userId+"', 'admin', 'test', 'testadmin', 'momo', 'momo', '"+userStatusId+"', '"+adminRoleId+"', now());");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var userId = migrationBuilder.Sql("SELECT id FROM users WHERE username = 'testadmin'");
            migrationBuilder.Sql("DELETE FROM users WHERE id = '"+userId+"';");
            
            migrationBuilder.Sql("DROP TABLE roles CASCADE");
            migrationBuilder.Sql("DROP TABLE statuses CASCADE");
        }
    }
}
