using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace RekognitionLab
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

            IdentifyFaces(fileName);
        }

        static void IdentifyFaces(string fileName)
        {
            var rekognitionClient = new AmazonRekognitionClient(Amazon.RegionEndpoint.EUWest1);

            var detectRequest = new DetectFacesRequest();

            var rekognitionImgage = new Amazon.Rekognition.Model.Image();

            byte[] data = null;

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                data = new byte[fileStream.Length];
                fileStream.Read(data, 0, (int)fileStream.Length);
            }

            rekognitionImgage.Bytes = new MemoryStream(data);

            detectRequest.Image = rekognitionImgage;

            var detectResponse = rekognitionClient.DetectFaces(detectRequest);

            var outputFile = string.Empty;

            if (detectResponse.FaceDetails.Count > 0)
            {
                // Load a bitmap to modify with face bounding box rectangles
                var facesHighlighted = new Bitmap(fileName);
                var pen = new Pen(Color.White, 3);

                // Create a graphics context
                using (var graphics = Graphics.FromImage(facesHighlighted))
                {
                    foreach (var faceDetail in detectResponse.FaceDetails)
                    {
                        // Get the bounding box
                        var boundingBox = faceDetail.BoundingBox;

                        Console.WriteLine("Bounding box = (" + boundingBox.Left + ", " + boundingBox.Top + ", " +
                            boundingBox.Height + ", " + boundingBox.Width + ")");

                        // Draw the rectangle using the bounding box values
                        // They are percentages so scale them to picture
                        graphics.DrawRectangle(pen, x: facesHighlighted.Width * boundingBox.Left,
                            y: facesHighlighted.Height * boundingBox.Top,
                            width: facesHighlighted.Width * boundingBox.Width,
                            height: facesHighlighted.Height * boundingBox.Height);
                    }

                    // Save the new image
                    outputFile = fileName.Replace(Path.GetExtension(fileName), "_faces.jpg");

                    facesHighlighted.Save(outputFile, ImageFormat.Jpeg);

                    Console.WriteLine(">>> " + detectResponse.FaceDetails.Count + " face(s) highlighted in file " + outputFile);
                }
            }
            else
            {
                Console.WriteLine("No faces have been detected!");
            }

            Console.WriteLine("The process is done");

            if (!string.IsNullOrWhiteSpace(outputFile))
            {
                Process.Start(outputFile);
            }
        }
    }
}
