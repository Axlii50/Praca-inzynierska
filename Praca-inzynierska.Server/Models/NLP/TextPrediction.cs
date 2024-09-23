using Microsoft.ML.Data;

namespace Praca_inzynierska.Server.Models.NLP
{
    public class TextPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Label { get; set; }
    }
}
