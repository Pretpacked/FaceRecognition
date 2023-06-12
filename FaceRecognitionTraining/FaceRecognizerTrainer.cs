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
            this.recognizer = new EigenFaceRecognizer();
            this.faceDatabase = this.LoadFaceDatabase();
        }

        public void Train()
        {
            List<Mat> faces = new List<Mat>();
            List<int> labels = new List<int>();

            for (int i = 0; i < faceDatabase.Count; i++)
            {
                faces.Add(faceDatabase[i].Mat);
                labels.Add(i);
            }

            recognizer.Train(faces.ToArray(), labels.ToArray());
        }

        private List<Image<Gray, byte>> LoadFaceDatabase()
        {
            List<Image<Gray, byte>> faceImages = new List<Image<Gray, byte>>();

            try
            {
                string path = detector.MyDocuments + "\\FaceDatabase";
                string[] files = Directory.GetFiles(
                    path, "*.jpg", System.IO.SearchOption.AllDirectories);

                foreach (string image in files)
                {
                    Image<Gray, byte> faceImage = new Image<Gray, byte>(image);
                    faceImages.Add(faceImage);
                }
            }
            catch (Exception ex) { System.Console.WriteLine(ex); }
                
            return faceImages;
        }
    }
}
