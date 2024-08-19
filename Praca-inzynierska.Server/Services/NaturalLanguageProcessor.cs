using Microsoft.AspNetCore.Components.Forms;
using Microsoft.ML;
using Microsoft.ML.Data;
using Praca_inzynierska.Server.Interfaces;
using System.Drawing;

namespace Praca_inzynierska.Server.Services
{
    public class NaturalLanguageProcessor : INaturalLanguageProcessor
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<InputData, OutputData> _predictionEngine;

        public NaturalLanguageProcessor()
        {
            _mlContext = new MLContext();

            // Tutaj należałoby załadować wcześniej wytrenowany model
            // To jest tylko przykład, w rzeczywistości potrzebny byłby prawdziwy model
            var model = _mlContext.Model.Load("path/to/your/model", out var inputSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<InputData, OutputData>(model);
        }

        public List<string> ExtractKeywords(string text)
        {
            var input = new InputData { Text = text };
            var output = _predictionEngine.Predict(input);

            // Zakładamy, że model zwraca prawdopodobieństwa dla różnych klas (słów kluczowych)
            return output.Predictions
                .Select((prob, index) => new { Keyword = $"Keyword{index}", Probability = prob })
                .Where(x => x.Probability > 0.5) // Filtrujemy tylko te z wysokim prawdopodobieństwem
                .Select(x => x.Keyword)
                .ToList();
        }

        public List<DetectedObject> DetectObjects(byte[] imageData, List<string> keywords)
        {
            // W rzeczywistości, ta metoda wymagałaby użycia modelu do detekcji obiektów
            // Tutaj przedstawiamy uproszczoną implementację
            var random = new Random();
            return keywords.Select(keyword => new DetectedObject
            {
                Label = keyword,
                BoundingBox = new Rectangle(
                    random.Next(0, 100),
                    random.Next(0, 100),
                    random.Next(50, 100),
                    random.Next(50, 100)
                )
            }).ToList();
        }
    }

    public class InputData
    {
        [LoadColumn(0)]
        public string Text;
    }

    public class OutputData
    {
        [VectorType(10)]
        public float[] Predictions;
    }
}
