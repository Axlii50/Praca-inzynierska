using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;

namespace Praca_inzynierska.Server.Services
{
    public class ImageProcessor : IImageProcessor
    {
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
    }

}
