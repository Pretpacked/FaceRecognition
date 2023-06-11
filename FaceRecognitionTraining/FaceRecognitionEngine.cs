using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System;

namespace FaceRecognitionTraining
{
    public class FaceRecognitionEngine
    {
        private EigenFaceRecognizer recognizer;

        public FaceRecognitionEngine()
        {
            this.recognizer = null;
        }

        public void SetRecognizer(EigenFaceRecognizer recognizer)
        {
            this.recognizer = recognizer;
        }

        public void RecognizeFacesInImage(Image<Gray, byte> image)
        {
            if (recognizer == null)
            {
                Console.WriteLine("Error: The recognizer is not set. Please set the recognizer before calling RecognizeFacesInImage.");
                return;
            }

            var result = recognizer.Predict(image.Mat);

            int predictedLabel = result.Label;
            double distance = result.Distance;

            Console.WriteLine("Predicted Label: " + predictedLabel);
            Console.WriteLine("Distance: " + distance);
        }
    }
}
