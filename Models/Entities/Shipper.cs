using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Northwind_API.Models.Entities;

public class Shipper
{
    public short ShipperId { get; set; }

    [Required]
    [MaxLength(40)]
    public string CompanyName { get; set; }

    [MaxLength(24)]
    public string? Phone { get; set; }

    // Navigation Properties
    [JsonIgnore]
    public List<Order> Orders { get; set; } = new();
}
