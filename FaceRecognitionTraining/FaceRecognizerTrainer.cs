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

        public FaceRecognizerTrainer()
        {
            this.faceDatabase = new List<Image<Gray, byte>>();
            this.recognizer = null;
        }

        public void AddTrainingImage(Image<Gray, byte> image)
        {
            faceDatabase.Add(image);
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

            recognizer = new EigenFaceRecognizer();
            recognizer.Train(faces.ToArray(), labels.ToArray());
        }

        public EigenFaceRecognizer GetRecognizer()
        {
            return recognizer;
        }
    }
}
