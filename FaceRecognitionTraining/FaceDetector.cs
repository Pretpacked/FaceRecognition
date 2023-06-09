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

        public FaceDetector(string cascadePath)
        {
            this.cascade = new CascadeClassifier(cascadePath);
        }

        public System.Drawing.Rectangle[] DetectFaces(Mat frame)
        {
            Mat grayFrame = new Mat();
            CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);
            return cascade.DetectMultiScale(grayFrame, 1.4, 0);
        }
    }
}
