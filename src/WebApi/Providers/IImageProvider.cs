namespace WebApi.Providers;

public interface IImageProvider
{
    Stream GetImage(int coasterId, bool reverse = false);
}