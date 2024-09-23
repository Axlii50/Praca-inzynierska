using Microsoft.AspNetCore.Mvc;
using Praca_inzynierska.Server.Interfaces;
using Praca_inzynierska.Server.Services;
using System.Diagnostics;

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

            //// Detekcja obiektów
            //var detectedObjects = _nlpProcessor.DetectObjects(imageData, keywords);

            //// Zaznaczenie obiektów na obrazie
            //var processedImageData = _imageProcessor.MarkObjects(imageData, detectedObjects);

            // Zwracanie przetworzonego obrazu
            //return File(processedImageData, "image/jpeg");
            return Ok();
        }
    }
}
