
namespace LogComponent.Services
{
    public interface IStreamWriterService
    {
        IStreamLogWriter GetStreamWriter(DateTime dateTime, string name);
    }
}