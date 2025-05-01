using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveysApi.Models;

[Table("statuses")]
public class Status
{
    [Column("id", TypeName = "char(36)"), MaxLength(36), Required, Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Column("code", TypeName = "varchar(50)"), MinLength(3), MaxLength(50), Required]
    public string Code { get; set; } = string.Empty;
    [Column("description", TypeName = "varchar(255)"), MinLength(3), MaxLength(255), Required]
    public string Description { get; set; } = string.Empty;
    [Column("created_at", TypeName = "timestamp"), Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }
    [Column("deleted_at", TypeName = "timestamp")]
    public DateTime? DeletedAt { get; set; }

    // Relationships
    public List<User>? Users { get; set; }
}
