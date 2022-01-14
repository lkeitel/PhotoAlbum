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

    public void StartApp()
    {
        _consoleIo.WriteLine("Enter an Album Id to retrieve, or press Q to quit: ");
        while (true)
        {
            var input = _consoleIo.ReadLine();

            if (input.ToLower() == "q")
            {
                return;
            }

            if (int.TryParse(input, out var albumRequest))
            {
                _consoleIo.WriteLine($"photo-album {albumRequest}");
                var photos = _photoServices.GetPhotos(albumRequest).Result;
                foreach (var photo in photos)
                {
                    _consoleIo.WriteLine($"[{photo.Id}] {photo.Title}");
                }

                _consoleIo.WriteLine("Enter another album Id or press Q to Quit:");
            }
            else
            {
                _consoleIo.WriteLine("Invalid Input. Try Another command, or press Q to Quit:");
            }
        }

    }

}