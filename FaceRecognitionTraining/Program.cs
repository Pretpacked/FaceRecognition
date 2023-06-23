using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
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

                trainer.TrainFaceRecognizer();

                manager.CreateWindow();

                Dictionary<int, string> labelMap = new Dictionary<int, string>();
                List<FaceImage> faceDatabase = trainer.faceDatabase;

                foreach (FaceImage faceImage in faceDatabase)
                {
                    int label = faceImage.GetLabel();
                    string name = faceImage.GetFullFileName();
                    labelMap[label] = name;
                }

                FaceRecognitionEngine engine = new FaceRecognitionEngine(trainer.GetRecognizer(), labelMap);

                while (true)
                {
                    Mat frame = manager.GetFrame();

                    if (CvInvoke.WaitKey(1) == 27)
                    {
                        manager.Release();
                        break;
                    }

                    System.Drawing.Rectangle[] faces = cascader.DetectFaces(frame);

                    foreach (System.Drawing.Rectangle face in faces)
                    {
                        CvInvoke.Rectangle(frame, face,
                            new Bgr(System.Drawing.Color.Red).MCvScalar);

                        Image<Bgr, byte> inputImage = frame.ToImage<Bgr, byte>();
                        inputImage = inputImage.Resize(100, 100, Emgu.CV.CvEnum.Inter.Linear);

                        Image<Gray, byte> grayImageX = inputImage.Convert<Gray, byte>();

                        engine.RecognizeFacesInImage(grayImageX);
                    }
                    CvInvoke.Imshow(manager.GetWindowName(), frame);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}