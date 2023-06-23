using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.IO;

namespace FaceRecognitionTraining
{
    public class FaceRecognizerTrainer
    {
        private List<FaceImage> faceDatabase;
        private EigenFaceRecognizer recognizer;
        private FaceDetector detector;

        public FaceRecognizerTrainer(FaceDetector detector)
        {
            this.faceDatabase = new List<FaceImage>();
            this.detector = detector;
            this.recognizer = new EigenFaceRecognizer();
            this.LoadFaceDatabase();
        }

        public EigenFaceRecognizer GetRecognizer()
        {
            return recognizer;
        }

        public List<FaceImage> GetFaceDatabase()
        {
            return faceDatabase;
        }

        private void LoadFaceDatabase()
        {
            string path = this.detector.GetDocumentsLocation() + "\\FaceDatabase";
            string[] directories = Directory.GetDirectories(path);

            int label = 1; // Start with label 1

            foreach (string directory in directories)
            {
                string folderName = Path.GetFileName(directory);
                string[] files = Directory.GetFiles(directory, "*.jpg");

                foreach (string file in files)
                {
                    Image<Gray, byte> faceImage = new Image<Gray, byte>(file);
                    FaceImage x = new FaceImage(faceImage, folderName, label);
                    this.faceDatabase.Add(x);
                }

                label++; // Increment label for the next folder
            }
        }




        public void TrainFaceRecognizer()
        {
            List<Mat> faces = new List<Mat>();
            List<int> labels = new List<int>();

            foreach (FaceImage faceImage in faceDatabase)
            {
                faces.Add(faceImage.GetImage().Mat);
                labels.Add(faceImage.GetLabel());
            }

            // Train face recognizer
            this.recognizer.Train(faces.ToArray(), labels.ToArray());
        }
    }
}
