namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations;

public class BaseModel
{
    [StringLength(64)]
    public string CreatedBy { get; set; } = "Unknown";

    public DateTime CreatedOn { get; set; }

    [StringLength(64)]
    public string UpdatedBy { get; set; } = "Unknown";

    public DateTime UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }

    [StringLength(64)]
    public string? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
}
