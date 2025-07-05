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
        if (!filePath.EndsWith(".tar.xz", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Expected a .tar.xz file", nameof(filePath));

        _logger.LogDebug("Decompressing and extracting tar.xz file: {FilePath} to {DestinationDir} ...", filePath, destinationDir);

        if (decompressedFileDir == null)
            decompressedFileDir = await _directoryUtil.CreateTempDirectory(cancellationToken).NoSync();

        string outputFilePath = Path.Combine(decompressedFileDir, Path.GetFileNameWithoutExtension(filePath) + ".tar");

        await _xzUtil.Decompress(filePath, outputFilePath, cancellationToken).NoSync();

        _tarUtil.Extract(outputFilePath, destinationDir, cancellationToken);

        if (deleteDecompressedFile)
            await _fileUtil.TryDelete(outputFilePath, cancellationToken: cancellationToken).NoSync();
    }
}