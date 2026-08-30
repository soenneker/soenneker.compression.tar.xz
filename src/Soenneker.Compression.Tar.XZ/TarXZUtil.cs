using Microsoft.Extensions.Logging;
using Soenneker.Compression.Tar.Abstract;
using Soenneker.Compression.Tar.XZ.Abstract;
using Soenneker.Compression.XZ.Abstract;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.Directory.Abstract;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Utils.File.Abstract;

namespace Soenneker.Compression.Tar.XZ;

/// <inheritdoc cref="ITarXZUtil"/>
public sealed class TarXZUtil : ITarXZUtil
{
    private readonly ITarUtil _tarUtil;
    private readonly IXZUtil _xzUtil;
    private readonly ILogger<TarXZUtil> _logger;
    private readonly IDirectoryUtil _directoryUtil;
    private readonly IFileUtil _fileUtil;

    public TarXZUtil(ITarUtil tarUtil, IXZUtil xzUtil, ILogger<TarXZUtil> logger, IDirectoryUtil directoryUtil, IFileUtil fileUtil)
    {
        _tarUtil = tarUtil;
        _xzUtil = xzUtil;
        _logger = logger;
        _directoryUtil = directoryUtil;
        _fileUtil = fileUtil;
    }

    public async ValueTask DecompressAndExtract(string filePath, string destinationDir, string? decompressedFileDir = null, bool deleteDecompressedFile = true,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Decompressing and extracting tar.xz file: {FilePath} to {DestinationDir} ...", filePath, destinationDir);

        bool ownsIntermediateDirectory = decompressedFileDir is null;

        if (ownsIntermediateDirectory)
            decompressedFileDir = await _directoryUtil.CreateTempDirectory(cancellationToken).NoSync();
        else
            await _directoryUtil.Create(decompressedFileDir!, true, cancellationToken).NoSync();

        string outputFilePath = Path.Combine(decompressedFileDir!, Path.GetFileNameWithoutExtension(filePath));

        try
        {
            await _xzUtil.Decompress(filePath, outputFilePath, cancellationToken).NoSync();

            await _tarUtil.Extract(outputFilePath, destinationDir, cancellationToken).NoSync();
        }
        finally
        {
            try
            {
                if (ownsIntermediateDirectory)
                    await _directoryUtil.DeleteIfExists(decompressedFileDir!, CancellationToken.None).NoSync();
                else if (deleteDecompressedFile)
                    await _fileUtil.TryDelete(outputFilePath, cancellationToken: CancellationToken.None).NoSync();
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "Could not remove the intermediate TAR output");
            }
        }
    }
}
