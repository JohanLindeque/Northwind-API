using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Northwind_API.Models.Entities;

public class Category
{
    public short CategoryId { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string CategoryName { get; set; }
    
    public string? Description { get; set; }
    
    public byte[]? Picture { get; set; }
    
}
