[![](https://img.shields.io/nuget/v/soenneker.compression.tar.xz.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.compression.tar.xz/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.compression.tar.xz/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.compression.tar.xz/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.compression.tar.xz.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.compression.tar.xz/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.compression.tar.xz/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.compression.tar.xz/actions/workflows/codeql.yml)

# Soenneker.Compression.Tar.XZ

Decompresses an XZ-compressed TAR archive and extracts its contents into a directory.

## Install

```bash
dotnet add package Soenneker.Compression.Tar.XZ
```

## Registration

```csharp
using Soenneker.Compression.Tar.XZ.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTarXZUtilAsSingleton();
```

Use `AddTarXZUtilAsScoped()` when its lifetime should follow a dependency-injection scope. Both registrations include the required TAR, XZ, file, and directory utilities.

## Usage

```csharp
using Soenneker.Compression.Tar.XZ.Abstract;

public sealed class PackageImporter(ITarXZUtil tarXzUtil)
{
    public ValueTask Import(string archivePath, string destinationPath, CancellationToken cancellationToken = default)
    {
        return tarXzUtil.DecompressAndExtract(archivePath, destinationPath, cancellationToken: cancellationToken);
    }
}
```

By default, the intermediate TAR is written to a private temporary directory and removed after extraction, including when extraction fails or is cancelled.

To control where the intermediate TAR is written, pass `decompressedFileDir`. Set `deleteDecompressedFile: false` only when you need to retain that TAR:

```csharp
await tarXzUtil.DecompressAndExtract(
    "backup.tar.xz",
    "extracted",
    decompressedFileDir: "work",
    deleteDecompressedFile: false,
    cancellationToken);
```

## Practical notes

- The destination directory is caller-owned. Files extracted before a failure or cancellation are not removed.
- The TAR extraction stage rejects links, path traversal, and destination collisions.
- Archive path checks do not impose expansion limits. Apply file-count, output-size, and storage quotas when handling untrusted archives.
