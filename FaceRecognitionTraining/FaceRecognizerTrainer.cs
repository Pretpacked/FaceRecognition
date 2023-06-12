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

        public FaceRecognizerTrainer(List<Image<Gray, byte>> faceDatabase)
        {
            this.faceDatabase = faceDatabase;
            this.recognizer = new EigenFaceRecognizer();
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
    }
}
