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
            List<Image<Gray, byte>> faceImages = new List<Image<Gray, byte>>();

            string path = this.detector.GetDocumentsLocation() + "\\FaceDatabase";

            if (Directory.Exists(path))
            {
                string[] folders = Directory.GetDirectories(path);

                foreach (string folderPath in folders)
                {
                    string parentFolderName = Path.GetFileName(folderPath);

                    string[] files = Directory.GetFiles(folderPath, "*.jpg");

                    foreach (string filePath in files)
                    {
                        Image<Gray, byte> image = new Image<Gray, byte>(filePath);
                        int label = int.Parse(Path.GetFileNameWithoutExtension(filePath));

                        faceDatabase.Add(new FaceImage(image, parentFolderName, label));
                    }
                }
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
