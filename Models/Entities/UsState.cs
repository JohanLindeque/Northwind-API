using System;
using System.ComponentModel.DataAnnotations;

namespace Northwind_API.Models.Entities;

public class UsState
{
    public short StateId { get; set; }
    
    [MaxLength(100)]
    public string? StateName { get; set; }
    
    [MaxLength(2)]
    public string? StateAbbr { get; set; }
    
    [MaxLength(50)]
    public string? StateRegion { get; set; }
}
