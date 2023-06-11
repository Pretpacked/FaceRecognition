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
        public string WindowName { get; }

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

        public void CreateWindow() {
            CvInvoke.NamedWindow(this.WindowName);
        }

        public void Release()
        {
            videoCapture.Dispose();
        }
    }
}
