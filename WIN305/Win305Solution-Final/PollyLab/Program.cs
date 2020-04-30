using System;
using System.Diagnostics;
using System.IO;

using Amazon.Polly;
using Amazon.Polly.Model;

namespace PollyLab
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Please provide text file, language code, and voice id.");

                return;
            }

            var fileName = args[0];
            var langCode = args[1];
            var voiceId = args[2];

            PlayAudioFromInputFile(fileName, langCode, voiceId);

        }

        static void PlayAudioFromInputFile(string fileName, string targetLanguageCode, string voiceId)
        {
            var text = File.ReadAllText(fileName);

            ConvertTextToAudio(fileName, text, targetLanguageCode, VoiceId.FindValue(voiceId));
        }

        static void ConvertTextToAudio(string fileName, string translatedText, string targetLanguageCode, VoiceId voice)
        {
            using (var pollyClient = new AmazonPollyClient(Amazon.RegionEndpoint.EUWest1))
            {
                var speechRequest = new SynthesizeSpeechRequest
                {
                    LanguageCode = targetLanguageCode,
                    Text = translatedText,
                    OutputFormat = OutputFormat.Mp3,
                    VoiceId = voice
                };

                var speechResponse = pollyClient.SynthesizeSpeechAsync(speechRequest).GetAwaiter().GetResult();

                string outputFileName = $"{fileName}-{targetLanguageCode}.mp3";

                FileStream output = File.Open(outputFileName, FileMode.Create);
                speechResponse.AudioStream.CopyTo(output);
                output.Close();

                Console.WriteLine("Saying..." + translatedText);

                Console.WriteLine();

                PlayAudio(outputFileName);
            }
        }

        public static void PlayAudio(string fileName)
        {
            string newFileName = fileName.Replace(".mp3", ".wav");

            ConvertMp3ToWav(fileName, newFileName);

            ProcessStartInfo startInfo = new
                ProcessStartInfo(@"powershell", $@"-c (New-Object Media.SoundPlayer '{newFileName}').PlaySync();")
            {
                WindowStyle = ProcessWindowStyle.Minimized
            };

            var process = Process.Start(startInfo);

            process.WaitForExit();
        }

        static void ConvertMp3ToWav(string fileName, string newFileName)
        {
            using (NAudio.Wave.Mp3FileReader mp3 = new NAudio.Wave.Mp3FileReader(fileName))
            {
                using (NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    NAudio.Wave.WaveFileWriter.CreateWaveFile(newFileName, pcm);
                }
            }
        }
    }
}
