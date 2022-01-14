using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using PhotoAlbum.Models;

namespace PhotoAlbum;

public class ConsoleService
{
    private IPhotoServices _photoServices;
    private IConsoleIO _consoleIo;
    public ConsoleService() : this(new PhotoServices(), new ConsoleIO())
    {
    }

    public ConsoleService(IPhotoServices photoServices, IConsoleIO consoleIo)
    {
        _photoServices = photoServices;
        _consoleIo = consoleIo;
    }

    public List<Photos> GetPhotos(int? albumId)
    {
        return _photoServices.GetPhotos(albumId).Result;
    }

    public void StartApp()
    {
        _consoleIo.WriteLine("Enter an Album Id to retrieve, or press Q to quit: ");
        _consoleIo.ReadLine();
        _photoServices.GetPhotos(1);
    }

}