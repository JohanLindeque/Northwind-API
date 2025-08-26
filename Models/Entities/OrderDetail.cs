using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Northwind_API.Models.Entities;

[Table("order_details")]
public class OrderDetail
{
    // Composite PK
    [Column("order_id")]
    public short OrderId { get; set; }

    [Column("product_id")]
    public short ProductId { get; set; }

    // Order detail Properties
    [Column("unit_price")]
    public float UnitPrice { get; set; }

    [Column("quantity")]
    public short Quantity { get; set; }

    [Column("discount")]
    public float Discount { get; set; }

    [ForeignKey("ProductId")]
    [JsonIgnore]
    public Product Product { get; set; } = null!;
}
