namespace Spotify.Application.Settings;

public class StreamFileAbstraction : TagLib.File.IFileAbstraction
{
    private readonly Stream _readStream;
    private readonly Stream _writeStream;

    public StreamFileAbstraction(string name, Stream readStream, Stream writeStream)
    {
        Name = name;
        _readStream = readStream;
        _writeStream = writeStream;
    }

    public string Name { get; }

    public Stream ReadStream => _readStream;

    public Stream WriteStream => _writeStream;

    public void CloseStream(Stream stream)
    {
    }
}
