using Soenneker.Compression.Tar.XZ.Abstract;
using Soenneker.Tests.Attributes.Local;
using Soenneker.Tests.HostedUnit;
using System.Threading.Tasks;

namespace Soenneker.Compression.Tar.XZ.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class TarXZUtilTests : HostedUnitTest
{
    private readonly ITarXZUtil _util;

    public TarXZUtilTests(Host host) : base(host)
    {
        _util = Resolve<ITarXZUtil>(true);
    }

    [Test]
    public void Default()
    {

    }

    [LocalOnly]
    public async ValueTask DecompressAndExtract()
    {
        await _util.DecompressAndExtract(@"c:\7zip\7zip.tar.xz", @"c:\dest", null, true, CancellationToken);
    }
}
