using static System.Net.Mime.MediaTypeNames;

namespace Praca_inzynierska.Server.Services
{
    public interface IImageProcessor
    {
        byte[] MarkObjects(byte[] imageData, List<DetectedObject> objects);
        IEnumerable<DetectedObject> DetectObjectsFromByteArray(byte[] imageData, string keyword);
    }
}
