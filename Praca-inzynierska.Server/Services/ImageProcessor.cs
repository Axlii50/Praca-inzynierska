using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using Microsoft.ML;
using Praca_inzynierska.Server.Models.NLP;
using static Tensorflow.Summary.Types;

namespace Praca_inzynierska.Server.Services
{
    public class ImageProcessor : IImageProcessor
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<byte[], List<DetectedObject>> _predictionEngine;

        public ImageProcessor()
        {
            _mlContext = new MLContext();

            var model = _mlContext.Model.Load("Model.zip", out var inputSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<byte[], List<DetectedObject>>(model);
        }

        public byte[] MarkObjects(byte[] imageData, List<DetectedObject> objects)
        {
            using (var mat = new Mat())
            {
                CvInvoke.Imdecode(imageData, ImreadModes.Color, mat);

                foreach (var obj in objects)
                {
                    // Rysowanie prostokąta
                    CvInvoke.Rectangle(mat, obj.BoundingBox, new MCvScalar(0, 0, 255), 2);

                    // Dodawanie etykiety
                    CvInvoke.PutText(
                        mat,
                        obj.Label,
                        new Point(obj.BoundingBox.X, obj.BoundingBox.Y - 10),
                        FontFace.HersheySimplex,
                        0.5,
                        new MCvScalar(0, 0, 255),
                        2
                    );
                }

                // Konwersja z powrotem do tablicy bajtów
                return mat.ToImage<Bgr, byte>().ToJpegData();
            }
        }

        public IEnumerable<DetectedObject> DetectObjectsFromByteArray(byte[] imageData, string keyword)
        {
            // Wykonaj predykcję
            //var prediction = _predictionEngine.Predict(imageData);

            //przefiltuj tylko te według słowa klucz
            //prediction = prediction.Where(x => x.Label == keyword).ToList();

            //return prediction;


            return new List<DetectedObject> { new DetectedObject() { Label = "kwiaty", BoundingBox = new Rectangle(10, 10, 100, 100) } };
        }
    }
}
