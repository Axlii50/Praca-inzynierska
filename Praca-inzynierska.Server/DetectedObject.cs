using System.Drawing;

namespace Praca_inzynierska.Server
{
    public class DetectedObject
    {
        public string Label { get; set; }
        public Rectangle BoundingBox { get; set; }
    }
}
