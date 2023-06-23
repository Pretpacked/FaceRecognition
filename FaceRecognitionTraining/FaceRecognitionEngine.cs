using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System;

namespace FaceRecognitionTraining
{
    public class FaceRecognitionEngine
    {
        private EigenFaceRecognizer recognizer;
        private FaceRecognizerTrainer trainer;

        public FaceRecognitionEngine(FaceRecognizerTrainer trainer)
        {
            this.trainer = trainer;
            this.recognizer = trainer.GetRecognizer();
        }

        public void RecognizeFacesInImage(Image<Gray, byte> image)
        {
            if (recognizer == null)
            {
                Console.WriteLine("Error: The recognizer is not set. Please set the recognizer before calling RecognizeFacesInImage.");
                return;
            }

            var result = recognizer.Predict(image.Resize(100, 100, Inter.Linear).Mat);

            int predictedLabel = result.Label;
            double distance = result.Distance;

            // Retrieve the face image from the faceDatabase using the predictedLabel
            var face = trainer.GetFaceDatabase().Find(f => f.GetLabel() == predictedLabel);
            string parentFolderName = face != null ? face.GetParentFolderName() : "Unknown";

            Console.WriteLine("Distance: " + distance);

            if (distance <= 5000) 
            {
                Console.WriteLine("Predicted Label: " + predictedLabel);
                Console.WriteLine("Parent Folder Name: " + parentFolderName);
            }
            else
            {
                Console.WriteLine("Not sure about the prediction. Accuracy is below the threshold.");
            }
        }

    }
}
