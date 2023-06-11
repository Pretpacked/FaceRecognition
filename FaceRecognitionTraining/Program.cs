using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;

namespace FaceRecognitionTraining
{
    public class Program
    {
        static void Main(string[] args)
        {
            VideoCaptureManager manager = new VideoCaptureManager();
            FaceDetector detector = new FaceDetector();
            FaceRecognizerTrainer trainer = new FaceRecognizerTrainer();
            FaceRecognitionEngine engine = new FaceRecognitionEngine();
            manager.CreateWindow();

            while (true)
            {
                Mat frame = manager.GetFrame();

                if (frame == null)
                    break;

                if (CvInvoke.WaitKey(1) == 27)
                {
                    manager.Release();
                    break;
                }

                System.Drawing.Rectangle[] faces = detector.DetectFaces(frame);

                if (faces.Length > 0)
                {
                    // Select the largest face
                    System.Drawing.Rectangle largestFace = faces[0];
                    int largestArea = largestFace.Width * largestFace.Height;

                    foreach (System.Drawing.Rectangle face in faces)
                    {
                        int area = face.Width * face.Height;
                        if (area > largestArea)
                        {
                            largestFace = face;
                            largestArea = area;
                        }
                    }

                    CvInvoke.Rectangle(frame, largestFace, new Bgr(System.Drawing.Color.Red).MCvScalar);

                    Mat grayImage = new Mat(frame, largestFace);
                    Image<Gray, byte> grayImageWithFace = grayImage.ToImage<Gray, byte>();

                    trainer.AddTrainingImage(grayImageWithFace);
                    grayImage.Dispose();
                }

                CvInvoke.Imshow(manager.WindowName, frame);
            }

            trainer.Train();
            EigenFaceRecognizer recognizer = trainer.GetRecognizer();
            engine.SetRecognizer(recognizer);

            while (true)
            {
                Mat frame = manager.GetFrame();

                if (frame == null)
                    break;

                if (CvInvoke.WaitKey(1) == 27)
                {
                    manager.Release();
                    break;
                }

                System.Drawing.Rectangle[] faces = detector.DetectFaces(frame);

                if (faces.Length > 0)
                {
                    // Select the largest face
                    System.Drawing.Rectangle largestFace = faces[0];
                    int largestArea = largestFace.Width * largestFace.Height;

                    foreach (System.Drawing.Rectangle face in faces)
                    {
                        int area = face.Width * face.Height;
                        if (area > largestArea)
                        {
                            largestFace = face;
                            largestArea = area;
                        }
                    }

                    CvInvoke.Rectangle(frame, largestFace, new Bgr(System.Drawing.Color.Red).MCvScalar);

                    Mat grayImage = new Mat(frame, largestFace);
                    Image<Gray, byte> grayImageWithFace = grayImage.ToImage<Gray, byte>();

                    engine.RecognizeFacesInImage(grayImageWithFace);
                    grayImage.Dispose();
                }

                CvInvoke.Imshow(manager.WindowName, frame);
            }

            CvInvoke.DestroyAllWindows();
        }
    }
}
