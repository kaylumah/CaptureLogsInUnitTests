// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Kaylumah.CaptureLogsInUnitTests.Echo;

public interface IEchoService
{
    Task<string> Echo(string input);
}
