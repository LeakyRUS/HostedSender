using Handlers.Abstractions.Config;
using System.Security.Cryptography;

namespace Hosted.Worker;

public class FileWatcher : IDisposable
{
    private bool _disposed = false;
    private FileSystemWatcher _watcher;
    private readonly IDefaultConfigCreator _creator;
    private readonly string _filePath;
    private string _fileHash;

    public event FileSystemEventHandler? OnChanged;

    public FileWatcher(string filePath, IDefaultConfigCreator defaultConfigCreator)
    {
        _creator = defaultConfigCreator;
        _filePath = filePath;

        _fileHash = CalculateHash(filePath);

        _watcher = new FileSystemWatcher();
        _watcher.Path = Path.GetDirectoryName(_filePath)!;
        _watcher.Filter = Path.GetFileName(_filePath);
        _watcher.NotifyFilter =
            NotifyFilters.LastWrite
            | NotifyFilters.FileName;

        _watcher.Changed += TrackChange;
        _watcher.Deleted += TrackDeleted;
        _watcher.Renamed += TrackRenamed;

        _watcher.EnableRaisingEvents = true;
    }

    ~FileWatcher()
    {
        Dispose(false);
    }

    private void TrackChange(object sender, FileSystemEventArgs e)
    {
        Thread.Sleep(100); // Ждем, когда освободится дескриптор файла, т. к. нормально проверить нельзя
        var changedHash = CalculateHash(e.FullPath);
        if (_fileHash != changedHash)
        {
            RunOnChange(sender, e);
            _fileHash = changedHash;
        }
    }

    private void TrackDeleted(object sender, FileSystemEventArgs e)
    {
        _creator.CreateIfNotExist(_filePath);
    }

    private void TrackRenamed(object sender, FileSystemEventArgs e)
    {
        _creator.CreateCopy(e.FullPath, _filePath);
    }

    private void RunOnChange(object sender, FileSystemEventArgs e)
    {
        if (!_disposed)
            OnChanged?.Invoke(sender, e);
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _watcher.Dispose();
        }

        _disposed = true;
    }

    private string CalculateHash(string fileName)
    {
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(fileName);
        var hash = md5.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}
