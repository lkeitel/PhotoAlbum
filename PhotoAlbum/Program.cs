using System;
using System.Collections.Generic;
using PhotoAlbum.Models;

namespace PhotoAlbum
{
    class Program
    {
        static void Main(string[] args)
        {
            PhotoServices services = new PhotoServices();
            Console.WriteLine("Enter an Album Id to retrieve, or press Q to quit: ");
            while (true)
            {
                var userInput = Console.ReadLine();
                int albumRequest;
                List<Photos> photos;
                if (userInput.ToLower() == "q")
                {
                    break;
                }
                else if (int.TryParse(userInput, out albumRequest))
                {
                    photos = services.GetPhotos(albumRequest).Result;
                    Console.WriteLine($"photo-album {albumRequest}");
                    foreach (var photo in photos)
                    {
                        Console.WriteLine($"[{photo.Id}] {photo.Title}");
                    }
                    Console.WriteLine("Enter another album Id or press Q to Quit:");
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Another command, or press Q to Quit:");
                }
            }
            
            
        }
    }
}
