using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionTraining
{
    public class VideoCaptureManager
    {
        private VideoCapture videoCapture;

        public VideoCaptureManager()
        {
            this.videoCapture = new VideoCapture(0);
        }

        public Mat GetFrame()
        {
            return videoCapture.QueryFrame();
        }

        public void Release()
        {
            videoCapture.Dispose();
        }
    }
}
