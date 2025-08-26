using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Northwind_API.Models.Entities;

public class Order
{
    [Column("order_id")]
    public short OrderId { get; set; }

    // Foreign Key Properties
    [Column("customer_id")]
    [MaxLength(5)]
    public string? CustomerId { get; set; }

    [Column("employee_id")]
    public short? EmployeeId { get; set; }

    [Column("ship_via")]
    public short? ShipVia { get; set; }

    // Order Properties
    [Column("order_date")]
    public DateTime? OrderDate { get; set; }

    [Column("required_date")]
    public DateTime? RequiredDate { get; set; }

    [Column("shipped_date")]
    public DateTime? ShippedDate { get; set; }

    [Column("freight")]
    public float? Freight { get; set; }

    [Column("ship_name")]
    [MaxLength(40)]
    public string? ShipName { get; set; }

    [Column("ship_address")]
    [MaxLength(60)]
    public string? ShipAddress { get; set; }

    [Column("ship_city")]
    [MaxLength(15)]
    public string? ShipCity { get; set; }

    [Column("ship_region")]
    [MaxLength(15)]
    public string? ShipRegion { get; set; }

    [Column("ship_postal_code")]
    [MaxLength(10)]
    public string? ShipPostalCode { get; set; }

    [Column("ship_country")]
    [MaxLength(15)]
    public string? ShipCountry { get; set; }

    // [ForeignKey("CustomerId")]
    // [JsonIgnore]
    // public Customer? Customer { get; set; }

    // [JsonIgnore]
    // public Employee? Employee { get; set; }

    [ForeignKey("ShipVia")]
    [JsonIgnore]
    public Shipper? Shipper { get; set; }

    [ForeignKey("OrderId")]
    [JsonIgnore]
    public List<OrderDetail> OrderDetails { get; set; } = new();
}
