using static System.Net.Mime.MediaTypeNames;
using Microsoft.ML;
using Praca_inzynierska.Server.Models.NLP;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using PointF = SixLabors.ImageSharp.PointF;
using SixLabors.Fonts;
using System.Drawing;
using Color = SixLabors.ImageSharp.Color;
using Font = SixLabors.Fonts.Font;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Diagnostics;
using Praca_inzynierska.Server.Models.ObjectDetection;
using Newtonsoft.Json;

namespace Praca_inzynierska.Server.Services
{
    public class ImageProcessor : IImageProcessor
    {
        string API_KEY = "x1LQbdKlYcIeYRgFrViX";
        string DATASET_NAME = "inz-gnlau";
        string DATASET_VERSION = "1";

        public ImageProcessor()
        {

        }

        public byte[] MarkObjects(byte[] imageData, List<DetectedObject> objects)
        {
            try
            {
                using (var image = SixLabors.ImageSharp.Image.Load(imageData))
                {
                    image.Mutate(x => x.AutoOrient());

                    foreach (var obj in objects)
                    {
                        image.Mutate(ctx =>
                        {
                            
                            // Rysowanie prostokąta
                            ctx.DrawPolygon(
                                     Color.Red,
                                     2f,
                                     new[] {
                                            new PointF(obj.BoundingBox.Left, obj.BoundingBox.Top),
                                            new PointF(obj.BoundingBox.Right, obj.BoundingBox.Top),
                                            new PointF(obj.BoundingBox.Right, obj.BoundingBox.Bottom),
                                            new PointF(obj.BoundingBox.Left, obj.BoundingBox.Bottom)
                                     }
                            );

                            var fontsize = CalculateFontSize(image.Bounds.Width, image.Bounds.Height);
                            // Dodawanie etykiety
                            var font = GetSafeFont(fontsize);
                            ctx.DrawText(obj.Label, font, Color.Red, new PointF(obj.BoundingBox.X + 5, obj.BoundingBox.Y + 5));
                        });
                    }

                    // Konwersja z powrotem do tablicy bajtów
                    using (var ms = new MemoryStream())
                    {
                        image.Save(ms, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MarkObjects: {ex.Message}");
                // Możesz zwrócić oryginalny obraz lub rzucić wyjątek
                return imageData;
            }
        }

        private static Font GetSafeFont(int fontSize)
        {
            FontCollection collection = new();
            FontFamily family = collection.Add("Roboto-Regular.ttf");
            return family.CreateFont(fontSize, FontStyle.Italic);
        }

        private static int CalculateFontSize(
                    double width,
                    double height,
                    double scaleFactor = 4.0,
                    double minFontSize = 8.0,
                    double maxFontSize = 72.0)
        {
            // Obliczamy bazowy rozmiar na podstawie szerokości i wysokości
            // Używamy średniej geometrycznej, która daje bardziej zbalansowane wyniki
            double baseSize = Math.Sqrt(width * height);

            // Stosujemy współczynnik skalowania
            // Dzielimy przez 100 żeby mieć bardziej intuicyjne wartości
            double fontSize = (baseSize / 100.0) * scaleFactor;

            // Zapewniamy, że rozmiar mieści się w zadanych granicach
            return (int)Math.Clamp(fontSize, minFontSize, maxFontSize);
        }

        public async Task<IEnumerable<DetectedObject>> DetectObjectsFromByteArray(byte[] imageData, string keyword)
        {
            string uploadURL ="https://detect.roboflow.com/" 
                + DATASET_NAME + "/" 
                + DATASET_VERSION 
                + "?api_key=" + API_KEY
                + $"&name={Guid.NewGuid().ToString()}.jpg";

            try
            {
                var responseContent = await UploadDataAsync(uploadURL, imageData);
                var predictions = DeserializeRespone(responseContent);

                var firstPredictionbasedOnKeyWord = predictions.predictions.First(x => x.className.ToUpper() == keyword.ToUpper());

                return new List<DetectedObject> { new DetectedObject() { Label = keyword,
                    BoundingBox = new System.Drawing.Rectangle(
                        (int)(firstPredictionbasedOnKeyWord.x - (firstPredictionbasedOnKeyWord.width / 2)),
                        (int)(firstPredictionbasedOnKeyWord.y - (firstPredictionbasedOnKeyWord.height / 2)),
                        (int)firstPredictionbasedOnKeyWord.width,
                        (int)firstPredictionbasedOnKeyWord.height) } };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("test");
                return null;
            }
        }

        private async Task<string> UploadDataAsync(string uploadUrl, byte[] data)
        {
            using (var httpClient = new HttpClient())
            {
                // Configure request
                var content = new ByteArrayContent(data);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                // Send request
                var response = await httpClient.PostAsync(uploadUrl, content);

                // Handle response
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public InferringModel DeserializeRespone(string json)
        {
            return JsonConvert.DeserializeObject<InferringModel>(json);
        }
    }
}
