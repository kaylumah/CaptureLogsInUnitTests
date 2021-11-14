// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;

namespace Test.Utilities.Logging.Xunit;

public sealed class XunitLoggerProvider : ILoggerProvider
{
    private readonly IDisposable _configurationOnChangeToken;
    private XunitLoggerConfiguration _currentConfiguration;
    private readonly ConcurrentDictionary<string, XunitLogger> _loggers = new();
    private readonly IExternalScopeProvider _externalScopeProvider = new LoggerExternalScopeProvider();
    private readonly ITestOutputHelper _testOutputHelper;

    public XunitLoggerProvider(IOptionsMonitor<XunitLoggerConfiguration> optionsMonitor, ITestOutputHelper testOutputHelper)
    {
        _currentConfiguration = optionsMonitor.CurrentValue;
        _configurationOnChangeToken = optionsMonitor.OnChange(updatedConfiguration => _currentConfiguration = updatedConfiguration);
        _testOutputHelper = testOutputHelper;
    }

    public ILogger CreateLogger(string categoryName)
    {
        var logger = _loggers.GetOrAdd(categoryName, name => new XunitLogger(name, GetCurrentConfiguration, _externalScopeProvider, _testOutputHelper));
        return logger;
    }

    public void Dispose()
    {
        _loggers.Clear();
        _configurationOnChangeToken.Dispose();
    }

    private XunitLoggerConfiguration GetCurrentConfiguration() => _currentConfiguration;
}
