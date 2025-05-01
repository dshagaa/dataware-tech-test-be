using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveysApi.Migrations
{
    /// <inheritdoc />
    public partial class addAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminRoleId = "7a47a457-1b96-4240-8289-d95b9469e6f3";
            var userStatusId = "bd964a3c-119c-4bb2-9fae-54e1a875476a";
            var userId = Guid.NewGuid().ToString();
            migrationBuilder.Sql("INSERT INTO users (id, first_name, last_name, username, password, token, status_id, created_at) VALUES ('"+userId+"', 'admin', 'test', 'testadmin', 'momo', 'momo', '"+userStatusId+"', now());");
            // migrationBuilder.Sql("INSERT INTO RoleUser (user_id, role_id) VALUES ('"+userId+"', '"+adminRoleId+"');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var userId = migrationBuilder.Sql("SELECT id FROM users WHERE username = 'testadmin'");
            // migrationBuilder.Sql("DELETE FROM RoleUser WHERE id = '"+userId+"';");
            migrationBuilder.Sql("DELETE FROM users WHERE id = '"+userId+"';");
        }
    }
}
