using System;

namespace Northwind_API.Helpers;

public static class ApiHelper
{
    public static string GenerateCorrelationId()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-dd-HH:mm");
    }





}
