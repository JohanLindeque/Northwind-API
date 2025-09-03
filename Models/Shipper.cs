using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Northwind_API.Models.Models;

[Table("shippers")]
public class Shipper
{
    [Key]
    [Column("shipper_id")]
    public short ShipperId { get; set; }

    [Column("company_name")]
    [Required]
    [MaxLength(40)]
    public string CompanyName { get; set; }

    [Column("phone")]
    [MaxLength(24)]
    public string? Phone { get; set; }

    // Navigation Properties
    [JsonIgnore]
    [ValidateNever]
    public List<Order> Orders { get; set; } = new();
}
