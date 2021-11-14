// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Kaylumah.CaptureLogsInUnitTests.Echo;
using Test.Utilities.Logging.Moq;
using Xunit;

namespace Test.Unit;

public class UnitTest3
{
    [Fact]
    public async Task Test_Moq_DefaultMockedLogger()
    {
        var loggerMock = LoggerMock<EchoService>.CreateDefault();
        var sut = new EchoService(loggerMock.Object);
        var testInput = "Scenario: mocked logger";
        var testResult = await sut.Echo(testInput).ConfigureAwait(false);
        testResult.Should().Be(testInput, "the input should have been returned");

        loggerMock.LogMessages.Should().NotBeEmpty().And.HaveCount(1);
        loggerMock.VerifyEventWasLogged(new EventId(1000));
    }

    [Fact]
    public async Task Test_Moq_LogLevelDisabledMockedLogger()
    {
        var loggerMock = LoggerMock<EchoService>.CreateDefault().SetupIsEnabled(LogLevel.Information, enabled: false);
        var sut = new EchoService(loggerMock.Object);
        var testInput = "Scenario: log level disabled mocked logger";
        var testResult = await sut.Echo(testInput).ConfigureAwait(false);
        testResult.Should().Be(testInput, "the input should have been returned");

        loggerMock.LogMessages.Should().BeEmpty();
    }
}
