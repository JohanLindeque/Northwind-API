using System;

namespace Northwind_API.Models;

public class ApiResponse<T> // T -> makes this class generic, works with any type 
{
    public bool Success { get; set; }
    public string RequestId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Message { get; set; }
    public T? Response { get; set; }


    public static ApiResponse<T> Result(string requestId, string message, T response)
    {
        return new ApiResponse<T>
        {
            Success = true,
            RequestId = requestId,
            TimeStamp = DateTime.UtcNow,
            Message = message,
            Response = response
        };
    }

    public static ApiResponse<T> ErrorResult(string requestId, string message, T response)
    {
        return new ApiResponse<T>
        {
            Success = false,
            RequestId = requestId,
            TimeStamp = DateTime.UtcNow,
            Message = message,
            Response = response
        };
    }
}
