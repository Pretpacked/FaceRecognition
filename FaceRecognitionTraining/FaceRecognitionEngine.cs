using Emgu.CV.Face;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;

namespace FaceRecognitionTraining
{
    public class FaceRecognitionEngine
    {
        private EigenFaceRecognizer recognizer;

        public FaceRecognitionEngine()
        {
            this.recognizer = new EigenFaceRecognizer();
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
