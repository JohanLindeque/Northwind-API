using System;
using System.Text.Json.Serialization;

namespace Northwind_API.Models.Entities;

public class OrderDetail
{
    // Composite PK
    public short OrderId { get; set; }
    public short ProductId { get; set; }
    

    public float UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }
    
    
    [JsonIgnore]
    public Product Product { get; set; } = null!;
}
