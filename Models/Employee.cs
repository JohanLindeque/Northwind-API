using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind_API.Models.Models;

public class Employee
{
    [Column("employee_id")]
    public short EmployeeId { get; set; }

    [Column("last_name")]
    [Required]
    [MaxLength(20)]
    public string LastName { get; set; }

    [Column("first_name")]
    [Required]
    [MaxLength(10)]
    public string FirstName { get; set; }

    [Column("title")]
    [MaxLength(30)]
    public string? Title { get; set; }

    [Column("title_of_courtesy")]
    [MaxLength(25)]
    public string? TitleOfCourtesy { get; set; }

    [Column("birth_date")]
    public DateTime? BirthDate { get; set; }

    [Column("hire_date")]
    public DateTime? HireDate { get; set; }

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

    [Column("home_phone")]
    [MaxLength(24)]
    public string? HomePhone { get; set; }

    [Column("extension")]
    [MaxLength(4)]
    public string? Extension { get; set; }

    [Column("photo")]
    public byte[]? Photo { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("reports_to")]
    public short? ReportsTo { get; set; }

    [Column("photo_path")]
    [MaxLength(255)]
    public string? PhotoPath { get; set; }
}
