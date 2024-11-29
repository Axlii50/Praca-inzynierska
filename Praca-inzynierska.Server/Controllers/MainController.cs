using Microsoft.AspNetCore.Mvc;
using Praca_inzynierska.Server.Interfaces;
using Praca_inzynierska.Server.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Praca_inzynierska.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly INaturalLanguageProcessor _nlpProcessor;
        private readonly IImageProcessor _imageProcessor;

        public MainController(INaturalLanguageProcessor nlpProcessor, IImageProcessor imageProcessor)
        {
            _nlpProcessor = nlpProcessor;
            _imageProcessor = imageProcessor;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessImage([FromForm] ProcessRequest request)
        {
            // Przetwarzanie tekstu
            var keyword = _nlpProcessor.ExtractKeywords(request.text);

            var convertedImage = await ConvertToJpgForLinux(request.File);

            // Wczytanie obrazu
            using var ms = new MemoryStream();
            await convertedImage.CopyToAsync(ms);
            var imageData = ms.ToArray();
            string encoded = Convert.ToBase64String(imageData);
            byte[] imageDataEncoded = Encoding.ASCII.GetBytes(encoded);

            //// Detekcja obiektów
            var detectedObjects = await _imageProcessor.DetectObjectsFromByteArray(imageDataEncoded, keyword);

            //if there is no detected object return oryginal image
            if(detectedObjects == null ||detectedObjects.Count() == 0) return File(imageData, "image/jpeg");

            //// Zaznaczenie obiektów na obrazie
            var processedImageData = _imageProcessor.MarkObjects(imageData, detectedObjects.ToList());

            // Zwracanie przetworzonego obrazu
            return File(processedImageData, "image/jpeg");
            //return Ok();
        }

        private async Task<IFormFile> ConvertToJpgForLinux(IFormFile file)
        {
            // Bezpieczna ścieżka dla Linuxa - użycie Path.Combine
            var tempFileName = Path.Combine(
                Path.GetTempPath(),
                $"{Guid.NewGuid()}_{Path.GetFileNameWithoutExtension(file.FileName)}.jpg"
            );

            try
            {
                // Sprawdzenie, czy plik nie jest pusty
                if (file == null || file.Length == 0)
                    throw new ArgumentException("Plik jest pusty");

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (var image = Image.Load(memoryStream))
                    {
                        // Opcjonalnie: zmniejszenie rozmiaru/kompresja
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(1920, 1080),
                            Mode = ResizeMode.Max
                        }));

                        // Zapis bezpośrednio na dysk dla Linuxa
                        image.Save(tempFileName, new JpegEncoder { Quality = 85 });
                    }
                }
                var fileStream = System.IO.File.OpenRead(tempFileName);
                var jpgFile = new FormFile(fileStream, 0, fileStream.Length,
                    Path.GetFileNameWithoutExtension(tempFileName),
                    Path.GetFileName(tempFileName));

                return jpgFile;
            }
            catch (Exception ex)
            {
                // Obsługa błędów charakterystyczna dla środowiska Linux
                Console.Error.WriteLine($"Błąd konwersji obrazu: {ex.Message}");
                throw;
            }
            finally
            {
                //fileStream.Close();
                //System.IO.File.Delete(tempFileName);
                // Opcjonalne czyszczenie plików tymczasowych
                // Dodaj własną logikę usuwania plików tymczasowych
            }
        }
    }
}
