// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Kaylumah.CaptureLogsInUnitTests.Echo;

public partial class EchoService : IEchoService
{
    private readonly ILogger<EchoService> _logger;

    public EchoService(ILogger<EchoService> logger)
    {
        _logger = logger;
    }

    public Task<string> Echo(string input)
    {
        //_logger.LogInformation("echo was invoked");

        // The logging message template should not vary between calls to 'LoggerExtensions.LogInformation(ILogger, string?, params object?[])' [Kaylumah.CaptureLogsInUnitTests.Echo]csharp(CA2254)
        // _logger.LogInformation($"echo was invoked with {input}");

        LogEchoCall(input);

        return Task.FromResult(input);
    }

    [LoggerMessage(1000, LogLevel.Information, "echo was invoked '{EchoInput}'")]
    partial void LogEchoCall(string echoInput);
}
