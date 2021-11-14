// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Utilities.Logging.Moq;

public class LoggerMock<TCategoryName> : Mock<ILogger<TCategoryName>>
{
    private readonly List<LogMessage> logMessages = new();

    public ReadOnlyCollection<LogMessage> LogMessages => new(logMessages);

    protected LoggerMock()
    {
    }

    public static LoggerMock<TCategoryName> CreateDefault()
    {
        return new LoggerMock<TCategoryName>()
            .SetupLog()
            .SetupIsEnabled(LogLevel.Information);
    }

    public LoggerMock<TCategoryName> SetupIsEnabled(LogLevel logLevel, bool enabled = true)
    {
        Setup(x => x.IsEnabled(It.Is<LogLevel>(p => p.Equals(logLevel))))
            .Returns(enabled);
        return this;
    }

    public LoggerMock<TCategoryName> SetupLog()
    {
        Setup(logger => logger.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => true),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)
        ))
        .Callback(new InvocationAction(invocation => {
            var logLevel = (LogLevel)invocation.Arguments[0];
            var eventId = (EventId)invocation.Arguments[1];
            var state = invocation.Arguments[2];
            var exception = (Exception?)invocation.Arguments[3];
            var formatter = invocation.Arguments[4];

            var invokeMethod = formatter.GetType().GetMethod("Invoke");
            var actualMessage = (string?)invokeMethod?.Invoke(formatter, new[] { state, exception });

            logMessages.Add(new LogMessage {
                EventId = eventId,
                LogLevel = logLevel,
                Message = actualMessage,
                Exception = exception,
                State = state
            });
        }));
        return this;
    }
}
