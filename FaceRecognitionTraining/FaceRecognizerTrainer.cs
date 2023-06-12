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
    public class FaceRecognizerTrainer
    {
        private List<Image<Gray, byte>> faceDatabase;
        private EigenFaceRecognizer recognizer;
        private FaceDetector detector;

        public FaceRecognizerTrainer()
        {
            this.detector = new FaceDetector();
            this.recognizer = new EigenFaceRecognizer();
            this.faceDatabase = this.LoadFaceDatabase();    
        }

        public void AddTrainingImage(Image<Gray, byte> image)
        {
            faceDatabase.Add(image);
        }
        public EigenFaceRecognizer GetRecognizer()
        {
            return recognizer;
        }

        private List<Image<Gray, byte>> LoadFaceDatabase()
        {
            List<Image<Gray, byte>> faceImages = new List<Image<Gray, byte>>();

            string path = this.detector.MyDocuments + "\\FaceDatabase";
            string[] files = Directory.GetFiles(
                path, "*.jpg", System.IO.SearchOption.AllDirectories);

            foreach (string image in files)
            {
                Image<Gray, byte> faceImage = new Image<Gray, byte>(image);
                faceImages.Add(faceImage);
            }

            return faceImages;
        }

        public void TrainFaceRecognizer()
        {
            List<Mat> faces = new List<Mat>();
            List<int> labels = new List<int>();

            for (int i = 0; i < this.faceDatabase.Count; i++)
            {
                System.Console.WriteLine(this.faceDatabase[i].ToString());
                faces.Add(this.faceDatabase[i].Mat);
                labels.Add(i);
            }

            // Train face recognizer
            this.recognizer.Train(faces.ToArray(), labels.ToArray());
        }
    }
}
