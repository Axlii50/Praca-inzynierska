using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;

class Program
{
    public class TextData
    {
        public string Text { get; set; }
        public string Label { get; set; }
    }

    public class TextPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Label { get; set; }
    }

    static async Task Main(string[] args)
    {
        var context = new MLContext();

        var trainData = new List<TextData>
        {
            // Papugi
            new TextData { Text = "znajdź mi papugę na tym zdjęciu", Label = "papugi" },
            new TextData { Text = "czy jest na tym zdjęciu papuga?", Label = "papugi" },
            new TextData { Text = "szukam papugi na tej fotografii", Label = "papugi" },
            new TextData { Text = "widziałem papugę w tym obrazie", Label = "papugi" },
            new TextData { Text = "proszę pokaż mi papugę na zdjęciu", Label = "papugi" },
            new TextData { Text = "czy na tym zdjęciu jest papuga", Label = "papugi" },
            new TextData { Text = "czy widzisz jakąś papugę na obrazku", Label = "papugi" },
            new TextData { Text = "pokaż mi, gdzie jest papuga", Label = "papugi" },
            new TextData { Text = "wskaż papugę na tej fotografii", Label = "papugi" },
            new TextData { Text = "zlokalizuj papugę w kadrze", Label = "papugi" },
            new TextData { Text = "czy mógłbyś znaleźć papugę na tej ilustracji", Label = "papugi" },
            new TextData { Text = "poproszę o wskazanie papugi na obrazku", Label = "papugi" },
            new TextData { Text = "bądź tak miły i pokaż mi, gdzie jest papuga", Label = "papugi" },
            new TextData { Text = "uprzejmie proszę o zlokalizowanie papugi na zdjęciu", Label = "papugi" },
            new TextData { Text = "czy na tym zdjęciu widać jakieś papugi w locie?", Label = "papugi" },
            new TextData { Text = "pokaż mi gniazdo papug, jeśli jest na obrazku", Label = "papugi" },
            new TextData { Text = "czy widzisz tu jakieś egzotyczne papugi?", Label = "papugi" },
            new TextData { Text = "znajdź mi papugę siedzącą na gałęzi", Label = "papugi" },
            new TextData { Text = "czy na tym zdjęciu jest jakaś ara?", Label = "papugi" },
            new TextData { Text = "pokaż mi, gdzie na obrazku są kakadu", Label = "papugi" },
           
            //Kapibara
            new TextData { Text = "znajdź mi kapibarę na tym zdjęciu", Label = "kapibara" },
            new TextData { Text = "czy jest na tym zdjęciu kapibara?", Label = "kapibara" },
            new TextData { Text = "szukam kapibary na tej fotografii", Label = "kapibara" },
            new TextData { Text = "widziałem kapibarę w tym obrazie", Label = "kapibara" },
            new TextData { Text = "proszę pokaż mi kapibarę na zdjęciu", Label = "kapibara" },
            new TextData { Text = "czy na tym zdjęciu jest kapibara", Label = "kapibara" },
            new TextData { Text = "czy widzisz jakąś kapibarę na obrazku", Label = "kapibara" },
            new TextData { Text = "pokaż mi, gdzie jest kapibara", Label = "kapibara" },
            new TextData { Text = "wskaż kapibarę na tej fotografii", Label = "kapibara" },
            new TextData { Text = "zlokalizuj kapibarę w kadrze", Label = "kapibara" },
            new TextData { Text = "czy mógłbyś znaleźć kapibarę na tej ilustracji", Label = "kapibara" },
            new TextData { Text = "poproszę o wskazanie kapibary na obrazku", Label = "kapibara" },
            new TextData { Text = "bądź tak miły i pokaż mi, gdzie jest kapibara", Label = "kapibara" },
            new TextData { Text = "uprzejmie proszę o zlokalizowanie kapibary na zdjęciu", Label = "kapibara" },
            new TextData { Text = "czy na tym zdjęciu widać jakieś kapibary w rzece?", Label = "kapibara" },
            new TextData { Text = "pokaż mi grupę kapibary, jeśli są na obrazku", Label = "kapibara" },
            new TextData { Text = "czy widzisz tu jakieś duże kapibary?", Label = "kapibary" },
            new TextData { Text = "znajdź mi kapibarę jedzącą trawę", Label = "kapibara" },
            new TextData { Text = "czy na tym zdjęciu jest jakaś kapibara?", Label = "kapibara" },
            new TextData { Text = "pokaż mi, gdzie na obrazku są kapibary", Label = "kapibara" },

            //Lwy
            new TextData { Text = "znajdź mi lwa na tym zdjęciu", Label = "lwy" },
            new TextData { Text = "czy jest na tym zdjęciu lew?", Label = "lwy" },
            new TextData { Text = "szukam lwów na tej fotografii", Label = "lwy" },
            new TextData { Text = "widziałem lwa w tym obrazie", Label = "lwy" },
            new TextData { Text = "proszę pokaż mi lwa na zdjęciu", Label = "lwy" },
            new TextData { Text = "czy na tym zdjęciu jest lew", Label = "lwy" },
            new TextData { Text = "czy widzisz jakiegoś lwa na obrazku", Label = "lwy" },
            new TextData { Text = "pokaż mi, gdzie jest lew", Label = "lwy" },
            new TextData { Text = "wskaż lwa na tej fotografii", Label = "lwy" },
            new TextData { Text = "zlokalizuj lwa w kadrze", Label = "lwy" },
            new TextData { Text = "czy mógłbyś znaleźć lwa na tej ilustracji", Label = "lwy" },
            new TextData { Text = "poproszę o wskazanie lwa na obrazku", Label = "lwy" },
            new TextData { Text = "bądź tak miły i pokaż mi, gdzie jest lew", Label = "lwy" },
            new TextData { Text = "uprzejmie proszę o zlokalizowanie lwa na zdjęciu", Label = "lwy" },
            new TextData { Text = "czy na tym zdjęciu widać jakieś lwy w trawie?", Label = "lwy" },
            new TextData { Text = "pokaż mi sforę lwów, jeśli są na obrazku", Label = "lwy" },
            new TextData { Text = "czy widzisz tu jakieś dumne lwy?", Label = "lwy" },
            new TextData { Text = "znajdź mi lwa śpiącego pod drzewem", Label = "lwy" },
            new TextData { Text = "czy na tym zdjęciu jest jakaś lwica?", Label = "lwy" },
            new TextData { Text = "pokaż mi, gdzie na obrazku są lwy", Label = "lwy" },

            //Panda
            new TextData { Text = "znajdź mi pandę na tym zdjęciu", Label = "panda" },
            new TextData { Text = "czy jest na tym zdjęciu panda?", Label = "panda" },
            new TextData { Text = "szukam pandy na tej fotografii", Label = "panda" },
            new TextData { Text = "widziałem pandę w tym obrazie", Label = "panda" },
            new TextData { Text = "proszę pokaż mi pandę na zdjęciu", Label = "panda" },
            new TextData { Text = "czy na tym zdjęciu jest panda", Label = "panda" },
            new TextData { Text = "czy widzisz jakąś pandę na obrazku", Label = "panda" },
            new TextData { Text = "pokaż mi, gdzie jest panda", Label = "panda" },
            new TextData { Text = "wskaż pandę na tej fotografii", Label = "panda" },
            new TextData { Text = "zlokalizuj pandę w kadrze", Label = "panda" },
            new TextData { Text = "czy mógłbyś znaleźć pandę na tej ilustracji", Label = "panda" },
            new TextData { Text = "poproszę o wskazanie pandy na obrazku", Label = "panda" },
            new TextData { Text = "bądź tak miły i pokaż mi, gdzie jest panda", Label = "panda" },
            new TextData { Text = "uprzejmie proszę o zlokalizowanie pandy na zdjęciu", Label = "panda" },
            new TextData { Text = "czy na tym zdjęciu widać jakieś pandy jedzące bambusa?", Label = "panda" },
            new TextData { Text = "pokaż mi grupę pand, jeśli są na obrazku", Label = "panda" },
            new TextData { Text = "czy widzisz tu jakieś urocze pandy?", Label = "panda" },
            new TextData { Text = "znajdź mi pandę siadającą na drzewie", Label = "panda" },
            new TextData { Text = "czy na tym zdjęciu jest jakaś panda?", Label = "panda" },
            new TextData { Text = "pokaż mi, gdzie na obrazku są pandy", Label = "panda" },

            //Slon
            new TextData { Text = "znajdź mi słonia na tym zdjęciu", Label = "slonie" },
            new TextData { Text = "czy jest na tym zdjęciu słoń?", Label = "slonie" },
            new TextData { Text = "szukam słoni na tej fotografii", Label = "slonie" },
            new TextData { Text = "widziałem słonia w tym obrazie", Label = "slonie" },
            new TextData { Text = "proszę pokaż mi słonia na zdjęciu", Label = "slonie" },
            new TextData { Text = "czy na tym zdjęciu jest słoń", Label = "slonie" },
            new TextData { Text = "czy widzisz jakiegoś słonia na obrazku", Label = "slonie" },
            new TextData { Text = "pokaż mi, gdzie jest słoń", Label = "slonie" },
            new TextData { Text = "wskaż słonia na tej fotografii", Label = "slonie" },
            new TextData { Text = "zlokalizuj słonia w kadrze", Label = "slonie" },
            new TextData { Text = "czy mógłbyś znaleźć słonia na tej ilustracji", Label = "slonie" },
            new TextData { Text = "poproszę o wskazanie słonia na obrazku", Label = "slonie" },
            new TextData { Text = "bądź tak miły i pokaż mi, gdzie jest słoń", Label = "slonyie" },
            new TextData { Text = "uprzejmie proszę o zlokalizowanie słonia na zdjęciu", Label = "slonie" },
            new TextData { Text = "czy na tym zdjęciu widać jakieś słonie kąpiące się w rzece?", Label = "slonie" },
            new TextData { Text = "pokaż mi stado słoni, jeśli są na obrazku", Label = "slonie" },
            new TextData { Text = "czy widzisz tu jakieś ogromne słonie?", Label = "slonie" },
            new TextData { Text = "znajdź mi słonia idącego przez sawannę", Label = "slonie" },
            new TextData { Text = "czy na tym zdjęciu jest jakaś samica słonia?", Label = "slonie" },
            new TextData { Text = "pokaż mi, gdzie na obrazku są słonie", Label = "slonie" },

            //Wydry
            new TextData { Text = "znajdź mi wydrę na tym zdjęciu", Label = "wydry" },
            new TextData { Text = "czy jest na tym zdjęciu wydra?", Label = "wydry" },
            new TextData { Text = "szukam wydr na tej fotografii", Label = "wydry" },
            new TextData { Text = "widziałem wydrę w tym obrazie", Label = "wydry" },
            new TextData { Text = "proszę pokaż mi wydrę na zdjęciu", Label = "wydry" },
            new TextData { Text = "czy na tym zdjęciu jest wydra", Label = "wydry" },
            new TextData { Text = "czy widzisz jakąś wydrę na obrazku", Label = "wydry" },
            new TextData { Text = "pokaż mi, gdzie jest wydra", Label = "wydry" },
            new TextData { Text = "wskaż wydrę na tej fotografii", Label = "wydry" },
            new TextData { Text = "zlokalizuj wydrę w kadrze", Label = "wydry" },
            new TextData { Text = "czy mógłbyś znaleźć wydrę na tej ilustracji", Label = "wydry" },
            new TextData { Text = "poproszę o wskazanie wydry na obrazku", Label = "wydry" },
            new TextData { Text = "bądź tak miły i pokaż mi, gdzie jest wydra", Label = "wydry" },
            new TextData { Text = "uprzejmie proszę o zlokalizowanie wydry na zdjęciu", Label = "wydry" },
            new TextData { Text = "czy na tym zdjęciu widać jakieś wydry płynące po rzece?", Label = "wydry" },
            new TextData { Text = "pokaż mi grupę wydr, jeśli są na obrazku", Label = "wydry" },
            new TextData { Text = "czy widzisz tu jakieś zabawne wydry?", Label = "wydry" },
            new TextData { Text = "znajdź mi wydrę siedzącą na kamieniu", Label = "wydry" },
            new TextData { Text = "czy na tym zdjęciu jest jakaś wydra?", Label = "wydry" },
            new TextData { Text = "pokaż mi, gdzie na obrazku są wydry", Label = "wydry" },
        };

        var trainDataView = context.Data.LoadFromEnumerable(trainData);


        // Tworzenie pipeline
        var pipeline = context.Transforms.Conversion.MapValueToKey("Label")
            .Append(context.Transforms.Text.FeaturizeText("Text", nameof(TextData.Text))
            .Append(context.Transforms.Concatenate("Features", "Text"))
            .Append(context.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                labelColumnName: "Label",
                maximumNumberOfIterations: 1000))
            .Append(context.Transforms.Conversion.MapKeyToValue("PredictedLabel")));

        // Trenowanie modelu
        var model = pipeline.Fit(trainDataView);


        // Przykładowe dane do predykcji
        var sampleText = new TextData[] {
            new TextData { Text = "znajdź mi pandę na tym zdjęciu" },
            new TextData { Text = "znajdź mi słonia" },
            new TextData { Text = "czy na tym zdjęciu jest wydra" },
            new TextData { Text = "czy to zdjęcie zawiera pandę" },
            new TextData { Text = "czy tu jest słoń" },
            new TextData { Text = "czy jest tu wydra" },
            new TextData { Text = "gdyby pandy potrafiły wspinać się na drzewa" },
            new TextData { Text = "czy słoń potrafi pływać" },
        };

        var predictor = context.Model.CreatePredictionEngine<TextData, TextPrediction>(model);

        foreach (var item in sampleText)
        {
            var prediction = predictor.Predict(item);

            // Wyświetlenie wyniku
            Console.WriteLine($"Predykcja: {prediction.Label}");
        }

        context.Model.Save(model, trainDataView.Schema, "Model.zip");
    }
}
