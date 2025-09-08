using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Northwind_API.Models.Models;

public class Product
{
    [Column("product_id")]
    public short ProductId { get; set; }

    [Column("product_name")]
    [Required]
    [MaxLength(40)]
    public string ProductName { get; set; }

    // Foreign Key Properties
    [Column("supplier_id")]
    public short? SupplierId { get; set; }

    [Column("category_id")]
    public short? CategoryId { get; set; }

    // Product Properties
    [Column("quantity_per_unit")]
    [MaxLength(20)]
    public string? QuantityPerUnit { get; set; }

    [Column("unit_price")]
    public float? UnitPrice { get; set; }

    [Column("units_in_stock")]
    public short? UnitsInStock { get; set; }

    [Column("units_on_order")]
    public short? UnitsOnOrder { get; set; }

    [Column("reorder_level")]
    public short? ReorderLevel { get; set; }

    [Column("discontinued")]
    public int Discontinued { get; set; }


}
