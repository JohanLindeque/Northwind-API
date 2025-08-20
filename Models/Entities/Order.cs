using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Northwind_API.Models.Entities;

public class Order
{
    public short OrderId { get; set; }
    
    // Foreign Key Properties
    [MaxLength(5)]
    public string? CustomerId { get; set; }
    public short? EmployeeId { get; set; }
    public short? ShipVia { get; set; }
    
    // Order Properties
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public float? Freight { get; set; }
    
    [MaxLength(40)]
    public string? ShipName { get; set; }
    [MaxLength(60)]
    public string? ShipAddress { get; set; }
    [MaxLength(15)]
    public string? ShipCity { get; set; }
    [MaxLength(15)]
    public string? ShipRegion { get; set; }
    [MaxLength(10)]
    public string? ShipPostalCode { get; set; }
    [MaxLength(15)]
    public string? ShipCountry { get; set; }
    

    [JsonIgnore]
    public Customer? Customer { get; set; }
    
    [JsonIgnore]
    public Employee? Employee { get; set; }
    
    [JsonIgnore]
    public Shipper? Shipper { get; set; }
    
    [JsonIgnore]
    public List<OrderDetail> OrderDetails { get; set; } = new();
}
