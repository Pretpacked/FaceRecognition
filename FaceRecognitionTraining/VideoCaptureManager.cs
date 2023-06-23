using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;

namespace FaceRecognitionTraining
{
    public class VideoCaptureManager
    {
        private VideoCapture videoCapture;
        private string WindowName;

        public VideoCaptureManager()
        {
            this.videoCapture = new VideoCapture(0);
            this.WindowName = "test window";
        }

        public Mat GetFrame()
        {
            Mat frame = this.videoCapture.QueryFrame();
            if (frame == null)
            {
                this.Release();
            }
            return frame;
        }

        public string GetWindowName() { return this.WindowName; }

        public void CreateWindow()
        {
            CvInvoke.NamedWindow(this.WindowName);
        }

        public void Release()
        {
            videoCapture.Dispose();
        }
    }
}
