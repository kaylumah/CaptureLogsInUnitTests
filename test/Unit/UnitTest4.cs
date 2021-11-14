// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using FluentAssertions;
using Kaylumah.CaptureLogsInUnitTests.Echo;
using Xunit;
using Xunit.Abstractions;

namespace Test.Unit;

public class UnitTest4
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest4(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Test_Custom_XunitLoggingBuilder()
    {
        var configuration = new ConfigurationBuilder().Build();
        var serviceProvider = new ServiceCollection()
            .AddLogging(loggingBuilder => {
                loggingBuilder.AddXunit(_testOutputHelper);
            })
            .AddEcho(configuration)
            .BuildServiceProvider();
        var sut = serviceProvider.GetRequiredService<IEchoService>();
        var testInput = "Scenario: custom logging builder";
        var testResult = await sut.Echo(testInput).ConfigureAwait(false);
        testResult.Should().Be(testInput, "the input should have been returned");
    }
}
