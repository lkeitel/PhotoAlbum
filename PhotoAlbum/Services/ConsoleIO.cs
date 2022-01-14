using System;

namespace PhotoAlbum;

public interface IConsoleIO
{
    void WriteLine(string s);
    string ReadLine();
}

public class ConsoleIO : IConsoleIO
{
    public void WriteLine(string s)
    {
        Console.WriteLine(s);
    }
    public string ReadLine()
    {
        return Console.ReadLine();
    }
}