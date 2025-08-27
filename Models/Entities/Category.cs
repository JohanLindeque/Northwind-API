using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Northwind_API.Models.Entities;

public class Category
{
    [Column("category_id")]
    public short CategoryId { get; set; }
    
    [Column("category_name")]
    [Required]
    [MaxLength(15)]
    public string CategoryName { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("picture")]
    public byte[]? Picture { get; set; }
    
}
