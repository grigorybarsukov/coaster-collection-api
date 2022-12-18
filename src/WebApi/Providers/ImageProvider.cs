using Microsoft.Extensions.Options;

namespace WebApi.Providers;

public class ImageProvider : IImageProvider
{
    private readonly ServiceConfiguration _serviceConfiguration;

    public ImageProvider(IOptions<ServiceConfiguration> serviceConfiguration)
    {
        _serviceConfiguration = serviceConfiguration.Value;
    }

    public Stream GetImage(int coasterId, bool reverse = false)
    {
        var path = Path.Join(_serviceConfiguration.ImagesDirectory, CreateFileName(coasterId, reverse));
        if (File.Exists(path) == false)
        {
            throw new FileNotFoundException();
        }
        var fileStream = new FileStream(path, FileMode.Open);
        return fileStream;
    }

    private string CreateFileName(int coasterId, bool reverse = false)
    {
        var subName = reverse ? "2" : "1";
        return $"{coasterId}_{subName}.png";
    }
}