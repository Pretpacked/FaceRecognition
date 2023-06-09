﻿using Emgu.CV.Face;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace FaceRecognitionTraining
{
    public class FaceRecognitionEngine
    {
        private EigenFaceRecognizer recognizer;

        public FaceRecognitionEngine(EigenFaceRecognizer recognizer)
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

            var result = recognizer.Predict(
                image.Resize(100, 100, Inter.Linear).Mat);

            int predictedLabel = result.Label;
            double distance = result.Distance;

            Console.WriteLine("Predicted Label: " + predictedLabel);
            Console.WriteLine("Distance: " + distance);
        }
    }
}
