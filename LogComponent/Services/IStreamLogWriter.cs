
namespace LogComponent.Services
{
    public interface IStreamLogWriter
    {
        void Dispose();
        Task WriteAsync(string text);
    }
}