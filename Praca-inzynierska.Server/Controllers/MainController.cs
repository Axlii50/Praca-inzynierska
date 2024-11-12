using Microsoft.AspNetCore.Mvc;
using Praca_inzynierska.Server.Interfaces;
using Praca_inzynierska.Server.Services;
using System.Diagnostics;
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

            // Wczytanie obrazu
            using var ms = new MemoryStream();
            await request.File.CopyToAsync(ms);
            var imageData = ms.ToArray();
            string encoded = Convert.ToBase64String(imageData);
            byte[] imageDataEncoded = Encoding.ASCII.GetBytes(encoded);

            //// Detekcja obiektów
            var detectedObjects = await _imageProcessor.DetectObjectsFromByteArray(imageDataEncoded, keyword);

            //if there is no detected object return oryginal image
            if(detectedObjects != null && detectedObjects.Count() == 0) return File(imageData, "image/jpeg");

            //// Zaznaczenie obiektów na obrazie
            var processedImageData = _imageProcessor.MarkObjects(imageData, detectedObjects.ToList());

            // Zwracanie przetworzonego obrazu
            return File(processedImageData, "image/jpeg");
            //return Ok();
        }
    }
}
