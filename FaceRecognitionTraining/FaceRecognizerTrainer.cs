using Emgu.CV.Face;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Drawing;

namespace FaceRecognitionTraining
{
    public enum LabelWithNames { };

    public class FaceRecognizerTrainer
    {
        private List<FaceImage> faceDatabase;
        private EigenFaceRecognizer recognizer;
        private FaceDetector detector;

        public FaceRecognizerTrainer(FaceDetector casader)
        {
            this.faceDatabase = new List<FaceImage>();
            this.detector = casader;
            this.recognizer = new EigenFaceRecognizer();
            this.LoadFaceDatabase();    
        }

        public EigenFaceRecognizer GetRecognizer()
        {
            return recognizer;
        }

        private void LoadFaceDatabase()
        {
            List<Image<Gray, byte>> faceImages = new List<Image<Gray, byte>>();

            string path = this.detector.GetDocumentsLocation() + "\\FaceDatabase";
            string[] files = Directory.GetFiles(
                path, "*.jpg", System.IO.SearchOption.AllDirectories);

            foreach (string image in files)
            {
                string[] fullFileName = Path.GetFileNameWithoutExtension(image).Split('_');

                Image<Gray, byte> faceImage = new Image<Gray, byte>(image);
                FaceImage x = new FaceImage(faceImage, fullFileName);
                this.faceDatabase.Add(x);
            }
        }

        public void TrainFaceRecognizer()
        {
            List<Mat> faces = new List<Mat>();
            List<int> labels = new List<int>();

            for (int i = 0; i < this.faceDatabase.Count; i++)
            {
                faces.Add(this.faceDatabase[i].GetImage().Mat);
                labels.Add(this.faceDatabase[i].GetLabel());
            }

            // Train face recognizer
            this.recognizer.Train(faces.ToArray(), labels.ToArray());
        }
    }
}
