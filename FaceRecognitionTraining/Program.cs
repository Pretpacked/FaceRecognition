using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace FaceRecognitionTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                VideoCaptureManager manager = new VideoCaptureManager();
                FaceDetector cascader = new FaceDetector();
                FaceRecognitionEngine engine = new FaceRecognitionEngine();
                manager.CreateWindow();

                while (true)
                {
                    Mat frame = manager.GetFrame();

                    if ((CvInvoke.WaitKey(1) & 0xFF) == 27) { manager.Release(); break; }

                    System.Drawing.Rectangle[] faces = cascader.DetectFaces(frame);

                    foreach (System.Drawing.Rectangle face in faces)
                    {
                        CvInvoke.Rectangle(frame, face, new Bgr(Color.Red).MCvScalar);

                        Image<Bgr, byte> inputImage = frame.ToImage<Bgr, byte>();
                        Image<Gray, byte> grayImageX = inputImage.Convert<Gray, byte>();

                        //engine.RecognizeFacesInImage(grayImageX);
                    }

                    CvInvoke.Imshow(manager.WindowName, frame);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
