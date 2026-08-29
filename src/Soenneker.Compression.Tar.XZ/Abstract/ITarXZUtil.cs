using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Compression.Tar.XZ.Abstract;

/// <summary>
/// A utility library dealing with Tar and XZ (tar.xz) extraction/archiving and (de)compression
/// </summary>
public interface ITarXZUtil
{
    /// <summary>
    /// Decompresses and Extract.
    /// </summary>
    /// <param name="filePath">Path of the file to use.</param>
    /// <param name="destinationDir">destination Dir that receives the result.</param>
    /// <param name="decompressedFileDir">Decompressed File Dir for the decompress and extract operation.</param>
    /// <param name="deleteDecompressedFile">Whether delete decompressed file.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the decompress and extract operation is complete.</returns>
    ValueTask DecompressAndExtract(string filePath, string destinationDir, string? decompressedFileDir = null, bool deleteDecompressedFile = true,
        CancellationToken cancellationToken = default);
}
