namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class HistoryLog : AutoHistory
{
    [StringLength(64)]
    public string Username { get; set; } = "unknown";
}
