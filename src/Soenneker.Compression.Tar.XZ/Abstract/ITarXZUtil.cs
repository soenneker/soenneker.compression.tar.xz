using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Compression.Tar.XZ.Abstract;

/// <summary>
/// A utility library dealing with Tar and XZ (tar.xz) extraction/archiving and (de)compression
/// </summary>
public interface ITarXZUtil
{
    ValueTask DecompressAndExtract(string filePath, string destinationDir, string? decompressedFileDir = null, bool deleteDecompressedFile = true,
        CancellationToken cancellationToken = default);
}