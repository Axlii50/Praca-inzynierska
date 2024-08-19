namespace Praca_inzynierska.Server
{
    public class DetectionRequest
    {
        public byte[] ImageData { get; set; }
        public List<string> Keywords { get; set; }
    }
}
