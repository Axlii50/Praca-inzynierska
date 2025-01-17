﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.ML;
using Praca_inzynierska.Server.Interfaces;
using Praca_inzynierska.Server.Models.NLP;
using System.Diagnostics;
using System.Drawing;

namespace Praca_inzynierska.Server.Services
{
    public class NaturalLanguageProcessor : INaturalLanguageProcessor
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<TextData, TextPrediction> _predictionEngine;

        public NaturalLanguageProcessor()
        {
            _mlContext = new MLContext();

            var model = _mlContext.Model.Load("Model.zip", out var inputSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<TextData, TextPrediction>(model);
        }

        public string ExtractKeywords(string text)
        {
            var prediction = _predictionEngine.Predict(new TextData { Text = text });

            return prediction.Label;
        }
    }
}
