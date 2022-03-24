using System;

public abstract class AbstractCSVFileReader : ICSVFileReader
{
    protected String GetFullPathWithoutExtension(String path)
    {
        return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path),
            System.IO.Path.GetFileNameWithoutExtension(path));
    }

    public abstract string ReadFile(string path);
}