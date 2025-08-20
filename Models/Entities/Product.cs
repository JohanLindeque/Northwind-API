using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Northwind_API.Models.Entities;

public class Product
{
    public short ProductId { get; set; }
    
    [Required]
    [MaxLength(40)]
    public string ProductName { get; set; }
    
    // Foreign Key Properties
    public short? SupplierId { get; set; }
    public short? CategoryId { get; set; }
    
    // Product Properties
    [MaxLength(20)]
    public string? QuantityPerUnit { get; set; }
    public float? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public int Discontinued { get; set; }
    
    // Navigation Properties
    [JsonIgnore]
    public Supplier? Supplier { get; set; }
    
    [JsonIgnore]
    public Category? Category { get; set; }
}
