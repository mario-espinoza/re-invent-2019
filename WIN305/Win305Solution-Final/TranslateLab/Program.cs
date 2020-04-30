using System;
using System.IO;

using Amazon.Translate;
using Amazon.Translate.Model;

namespace TranslateLab
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide text file name (English text) that you want to translate.");

                return;
            }

            var fileName = args[0];

            TranslateInputFile(fileName);

            Console.WriteLine("The process is complete");
        }

        static void TranslateInputFile(string fileName)
        {
            var text = File.ReadAllText(fileName);

            string polishTranslated = TranslateFromEnglish(text, "pl");

            string spanishTranslated = TranslateFromEnglish(text, "es");

            string germanTranslated = TranslateFromEnglish(text, "de");

            string frenchTranslated = TranslateFromEnglish(text, "fr");

            var polishOutputFileName = fileName.Replace(Path.GetExtension(fileName), "_polish.txt");
            File.WriteAllText(polishOutputFileName, polishTranslated);

            var spanishOutputFileName = fileName.Replace(Path.GetExtension(fileName), "_spanish.txt");
            File.WriteAllText(spanishOutputFileName, spanishTranslated);

            var germanOutputFileName = fileName.Replace(Path.GetExtension(fileName), "_germanh.txt");
            File.WriteAllText(germanOutputFileName, germanTranslated);

            var frenchOutputFileName = fileName.Replace(Path.GetExtension(fileName), "_french.txt");
            File.WriteAllText(frenchOutputFileName, frenchTranslated);
        }

        static string TranslateFromEnglish(string toTranslate, string targetLanguageCode)
        {
            string outputString;

            using (var translateClient = new AmazonTranslateClient(Amazon.RegionEndpoint.EUWest1))
            {
                var translatRequest = new TranslateTextRequest
                {
                    Text = toTranslate,
                    SourceLanguageCode = "EN",
                    TargetLanguageCode = targetLanguageCode
                };

                var translatResponse = translateClient.TranslateTextAsync(translatRequest).GetAwaiter().GetResult();

                outputString = translatResponse?.TranslatedText;

                Console.WriteLine(outputString);
                Console.WriteLine();

            }

            return outputString;
        }
    }
}
