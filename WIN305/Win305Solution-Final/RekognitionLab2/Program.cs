using System;
using System.Diagnostics;
using System.IO;
using System.Text;

using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace RekognitionLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide picture file name!");

                return;
            }

            var fileName = args[0];

            DetectScenes(fileName);
        }

        static void DetectScenes(string fileName)
        {
            var rekognitionClient = new AmazonRekognitionClient(Amazon.RegionEndpoint.EUWest1);

            var detectLabelsRequest = new DetectLabelsRequest();

            var rekognitionImgage = new Amazon.Rekognition.Model.Image();

            byte[] data = null;

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                data = new byte[fileStream.Length];
                fileStream.Read(data, 0, (int)fileStream.Length);
            }

            rekognitionImgage.Bytes = new MemoryStream(data);

            detectLabelsRequest.Image = rekognitionImgage;

            var detectLabelsResponse = rekognitionClient.DetectLabels(detectLabelsRequest);

            var outputFileName = string.Empty;

            if (detectLabelsResponse.Labels.Count > 0)
            {
                var content = new StringBuilder();
                content.Append("This report is based on Amazon Rekognition's objects and scenes detection capability. ");
                content.Append("This service detects the objects and scenes in the image and returns them along with ");
                content.Append("a percent confidence score for each object and scene");
                content.AppendLine("");
                content.Append("Amazon Rekognition detects the following objects and scenes in the image provided:");
                content.AppendLine("");

                foreach (var item in detectLabelsResponse.Labels)
                {
                    content.AppendLine(item.Name + " with the confidence " + Convert.ToInt32(item.Confidence) + "%");
                }

                content.AppendLine("");
                content.Append(string.Format("End of this report, created on {0}", DateTime.Now));
                content.AppendLine("");

                outputFileName = fileName.Replace(Path.GetExtension(fileName), "_report.txt");

                File.WriteAllText(outputFileName, content.ToString());
            }
            else
            {
                Console.WriteLine("No objects have been detected!");
            }

            Console.WriteLine("The process is done");

            if (!string.IsNullOrWhiteSpace(outputFileName))
            {
                Process.Start(outputFileName);
            }
        }
    }
}
