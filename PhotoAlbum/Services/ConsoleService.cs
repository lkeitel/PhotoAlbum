using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        var input = _consoleIo.ReadLine();
        int albumRequest;
         
        if (input.ToLower() == "q")
        {
            return;
        }
        else if (int.TryParse(input, out albumRequest))
        {
            _consoleIo.WriteLine($"photo-album {albumRequest}");
            _photoServices.GetPhotos(albumRequest);
            _consoleIo.WriteLine("Enter another album Id or press Q to Quit:");
        }
        else
        {
            _consoleIo.WriteLine("Invalid Input. Try Another command, or press Q to Quit:");
        }
            
    }

}