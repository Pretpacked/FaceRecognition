using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceRecognitionTraining
{
	public class VideoFeed
	{
		public VideoFeed()
		{
            String win1 = "Test Window (Press any key to close)"; //The name of the window
            CvInvoke.NamedWindow(win1); //Create the window using the specific name
            using (Mat frame = new Mat())
            using (this.GetCamera())
                while (CvInvoke.WaitKey(1) == -1)
                {
                    this.GetCamera().Read(frame);
                    CvInvoke.Imshow(win1, frame);
                }
        }

        public VideoCapture GetCamera()
        {
            return new VideoCapture(0);
        }
	}
}


