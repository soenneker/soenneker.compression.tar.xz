using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Compression.Tar.XZ.Abstract;

/// <summary>
/// A utility library dealing with Tar and XZ (tar.xz) extraction/archiving and (de)compression
/// </summary>
public interface ITarXZUtil
{
    /// <summary>
    /// Executes the decompress and extract operation.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="destinationDir">The destination dir.</param>
    /// <param name="decompressedFileDir">The decompressed file dir.</param>
    /// <param name="deleteDecompressedFile">The delete decompressed file.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask DecompressAndExtract(string filePath, string destinationDir, string? decompressedFileDir = null, bool deleteDecompressedFile = true,
        CancellationToken cancellationToken = default);
}