using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Praca_inzynierska.Server.Models.ObjectDetection
{
    public class Predictions
    {
        public float x;

        public float y;

        public float width;

        public float height;

        public float confidence;

        [JsonProperty("class")]
        public string className;

        [JsonProperty("class_id")]
        public int classId;

        [JsonProperty("detection_id")]
        public Guid detectionId;
    }
}