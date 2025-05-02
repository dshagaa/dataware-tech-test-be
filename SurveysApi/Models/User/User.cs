using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveysApi.Models;

[Table("users")]
public class User
{
    [Column("id", TypeName = "char(36)"), MaxLength(36), Required, Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Column("first_name", TypeName = "varchar(50)"), MaxLength(50), Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters.")]
    public string FirstName { get; set; } = string.Empty;
    [Column("last_name", TypeName = "varchar(50)"), MinLength(3), MaxLength(50), Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only contain letters.")]
    public string LastName { get; set; } = string.Empty;
    [Column("username", TypeName = "varchar(50)"), MinLength(3), MaxLength(50), Required]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and numbers.")]
    public string Username { get; set; } = string.Empty;
    [Column("password", TypeName = "varchar(255)"), MaxLength(255)]
    public string Password { get; set; } = string.Empty;
    [Column("token", TypeName = "text")]
    public string? Token { get; set; } = string.Empty;
    [Column("recovery_token", TypeName = "text")]
    public string? RecoveryToken { get; set; } = string.Empty;
    [Column("status_id", TypeName = "char(36)"), MaxLength(36), Required]
    public string StatusId { get; set; } = string.Empty;
    [Column("role_id", TypeName = "char(36)"), MaxLength(36), Required]
    public string RoleId { get; set; } = string.Empty;
    [Column("created_at", TypeName = "timestamp"), Required]
    public DateTime CreatedAt { get; set; } = DateTimeOffset.UtcNow.DateTime;
    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }
    [Column("deleted_at", TypeName = "timestamp")]
    public DateTime? DeletedAt { get; set; }

    // Relationships
    public Role? Role { get; set; }
    public Status? Status { get; set; }
    public List<Survey>? Surveys { get; set; }
}
