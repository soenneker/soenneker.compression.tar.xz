using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Compression.Tar.XZ.Abstract;

/// <summary>
/// Decompresses XZ-compressed TAR archives and extracts their contents.
/// </summary>
public interface ITarXZUtil
{
    /// <summary>
    /// Decompresses an XZ stream to an intermediate TAR file, then extracts the TAR into a destination directory.
    /// </summary>
    /// <param name="filePath">Path to the XZ-compressed TAR archive.</param>
    /// <param name="destinationDir">Directory that receives the extracted files.</param>
    /// <param name="decompressedFileDir">Optional directory for the intermediate TAR file. A temporary directory is used when omitted.</param>
    /// <param name="deleteDecompressedFile">Whether to delete the intermediate TAR when <paramref name="decompressedFileDir"/> is supplied.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the decompress-and-extract operation.</returns>
    /// <remarks>An internally created temporary directory is always removed. A caller-selected intermediate directory is never removed.</remarks>
    ValueTask DecompressAndExtract(string filePath, string destinationDir, string? decompressedFileDir = null, bool deleteDecompressedFile = true,
        CancellationToken cancellationToken = default);
}
