using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveysApi.Models;

[Table("surveys")]
public class Survey
{
    [Column("id", TypeName = "char(36)"), MaxLength(36), Required, Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Column("user_id", TypeName = "char(36)"), MaxLength(36), Required]
    public string UserId { get; set; } = string.Empty;
    [Column("title", TypeName = "varchar(255)"), MinLength(3), MaxLength(255), Required]
    public string Title { get; set; } = string.Empty;
    [Column("description", TypeName = "varchar(255)"), MinLength(3), MaxLength(255), Required]
    public string Description { get; set; } = string.Empty;
    [Column("registration_start_date", TypeName = "timestamp")]
    public DateTime? RegistrationStartDate { get; set; }
    [Column("registration_end_date", TypeName = "timestamp")]
    public DateTime? RegistrationEndDate { get; set; }
    [Column("start_date", TypeName = "timestamp")]
    public DateTime? StartDate { get; set; }
    [Column("end_date", TypeName = "timestamp")]
    public DateTime? EndDate { get; set; }
    [Column("configuration", TypeName = "json")]
    public object? configuration { get; set; }
    [Column("metadata", TypeName = "json")]
    public object? Metadata { get; set; }
    [Column("created_at", TypeName = "timestamp"), Required]
    public DateTime CreatedAt { get; set; } = DateTimeOffset.UtcNow.DateTime;
    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }
    [Column("deleted_at", TypeName = "timestamp")]
    public DateTime? DeletedAt { get; set; }

    // Relationships
    public User? User { get; set; }
}
