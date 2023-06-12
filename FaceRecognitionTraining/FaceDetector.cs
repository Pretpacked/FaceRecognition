using Emgu.CV.CvEnum;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionTraining
{
    public class FaceDetector
    {
        private CascadeClassifier cascade;
        public string MyDocuments = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments);

        public FaceDetector()
        {
            this.cascade = new CascadeClassifier(
                this.MyDocuments + "\\haarcascade_frontalface_alt.xml");
        }

        public System.Drawing.Rectangle[] DetectFaces(Mat frame)
        {
            Mat grayFrame = new Mat();
            CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);

            double scaleFactor = 1.1;
            int minNeighbors = 3;

            return cascade.DetectMultiScale(grayFrame, scaleFactor, minNeighbors);
        }
    }
}
