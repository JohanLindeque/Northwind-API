using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Northwind_API.Models.Models;

public class Customer
{
    [Column("customer_id")]
    [Required]
    [MaxLength(5)]
    public string CustomerId { get; set; }

    [Column("company_name")]
    [Required]
    [MaxLength(40)]
    public string CompanyName { get; set; }

    [Column("contact_name")]
    [MaxLength(30)]
    public string? ContactName { get; set; }

    [Column("contact_title")]
    [MaxLength(30)]
    public string? ContactTitle { get; set; }

    [Column("address")]
    [MaxLength(60)]
    public string? Address { get; set; }

    [Column("city")]
    [MaxLength(15)]
    public string? City { get; set; }

    [Column("region")]
    [MaxLength(15)]
    public string? Region { get; set; }

    [Column("postal_code")]
    [MaxLength(10)]
    public string? PostalCode { get; set; }

    [Column("country")]
    [MaxLength(15)]
    public string? Country { get; set; }

    [Column("phone")]
    [MaxLength(24)]
    public string? Phone { get; set; }

    [Column("fax")]
    [MaxLength(24)]
    public string? Fax { get; set; }


}
