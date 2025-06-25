using Soenneker.Compression.Tar.XZ.Abstract;
using Soenneker.Facts.Local;
using Soenneker.Tests.FixturedUnit;
using System.Threading.Tasks;
using Xunit;

namespace Soenneker.Compression.Tar.XZ.Tests;

[Collection("Collection")]
public sealed class TarXZUtilTests : FixturedUnitTest
{
    private readonly ITarXZUtil _util;

    public TarXZUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ITarXZUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }

    [LocalFact]
    public async ValueTask DecompressAndExtract()
    {
        await _util.DecompressAndExtract(@"c:\7zip\7zip.tar.xz", @"c:\dest", null, true, CancellationToken);
    }
}
