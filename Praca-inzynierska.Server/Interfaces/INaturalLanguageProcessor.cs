namespace Praca_inzynierska.Server.Interfaces
{
    public interface INaturalLanguageProcessor
    {
        List<string> ExtractKeywords(string text);
        List<DetectedObject> DetectObjects(byte[] imageData, List<string> keywords);
    }
}
