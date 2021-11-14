// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit.Abstractions;

namespace Test.Utilities.Logging.Xunit;

public class XunitLogger : ILogger
{
    private readonly string _loggerName;
    private readonly Func<XunitLoggerConfiguration> _getCurrentConfig;
    private readonly IExternalScopeProvider _externalScopeProvider;
    private readonly ITestOutputHelper _testOutputHelper;

    public XunitLogger(string loggerName, Func<XunitLoggerConfiguration> getCurrentConfig, IExternalScopeProvider externalScopeProvider, ITestOutputHelper testOutputHelper)
    {
        _loggerName = loggerName;
        _getCurrentConfig = getCurrentConfig;
        _externalScopeProvider = externalScopeProvider;
        _testOutputHelper = testOutputHelper;
    }

    public IDisposable BeginScope<TState>(TState state) => _externalScopeProvider.Push(state);

    public bool IsEnabled(LogLevel logLevel) => LogLevel.None != logLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

         var message = formatter(state, exception);
         _testOutputHelper.WriteLine(message);
    }
}
