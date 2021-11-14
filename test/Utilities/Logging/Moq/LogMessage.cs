// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Logging;

namespace Test.Utilities.Logging.Moq;

public class LogMessage
{
    public EventId EventId { get; set; }

    public Exception? Exception { get; set; }

    public LogLevel LogLevel { get; set; }

    public string? Message { get; set; }

    public object? State { get; set; }
}
