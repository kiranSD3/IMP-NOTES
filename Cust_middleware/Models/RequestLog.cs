using System;
using System.Collections.Generic;

namespace Cust_middleware.Models;

public partial class RequestLog
{
    public int Id { get; set; }

    public string Method { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string? QueryString { get; set; }

    public int StatusCode { get; set; }

    public string? UserName { get; set; }

    public string? IpAddress { get; set; }

    public long ElapsedMilliseconds { get; set; }

    public DateTime Timestamp { get; set; }
}
