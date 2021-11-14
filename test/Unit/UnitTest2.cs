// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using FluentAssertions;
using Kaylumah.CaptureLogsInUnitTests.Echo;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Test.Unit;

public class UnitTest2
{
    [Fact]
    public async Task Test_Manuel_EmptyLoggingFactory()
    {
        var logger = new LoggerFactory().CreateLogger<EchoService>();
        var sut = new EchoService(logger);
        var testInput = "Scenario: empty logger factory";
        var testResult = await sut.Echo(testInput).ConfigureAwait(false);
        testResult.Should().Be(testInput, "the input should have been returned");
    }

    [Fact]
    public async Task Test_Manuel_NullLoggingFactory()
    {
        var sut = new EchoService(NullLogger<EchoService>.Instance);
        var testInput = "Scenario: null logger factory";
        var testResult = await sut.Echo(testInput).ConfigureAwait(false);
        testResult.Should().Be(testInput, "the input should have been returned");
    }
}
