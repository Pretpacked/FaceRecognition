using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Linq;

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
                FaceRecognizerTrainer trainer = new FaceRecognizerTrainer(cascader);
                FaceRecognitionEngine engine = new FaceRecognitionEngine(trainer);

                trainer.TrainFaceRecognizer();

                manager.CreateWindow();

                while (true)
                {
                    Mat frame = manager.GetFrame();

                    if (CvInvoke.WaitKey(1) == 27) { manager.Release(); break; }

                    System.Drawing.Rectangle[] faces = cascader.DetectFaces(frame);

                    foreach (System.Drawing.Rectangle face in faces)
                    {
                        CvInvoke.Rectangle(frame, face, new Bgr(System.Drawing.Color.Red).MCvScalar);

                        Image<Bgr, byte> inputImage = frame.ToImage<Bgr, byte>();
                        inputImage.ROI = face;
                        Image<Gray, byte> grayImageX = inputImage.Convert<Gray, byte>();

                        engine.RecognizeFacesInImage(grayImageX);
                    }

                    CvInvoke.Imshow(manager.GetWindowName(), frame);
                }

                CvInvoke.DestroyAllWindows();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
