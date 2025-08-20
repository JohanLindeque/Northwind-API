using System;

namespace Northwind_API.Models.API;

public class ApiResponse<T> // makes class generic, works with any type 
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}
