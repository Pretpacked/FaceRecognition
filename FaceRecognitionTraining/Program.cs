using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Face;
using Emgu.CV.CvEnum;
using FaceRecognitionTraining;
using System.Drawing;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Presentation;
using Emgu.CV.Dai;
using System;
using DocumentFormat.OpenXml.Wordprocessing;

//VideoFeed x = new VideoFeed();


//x.timer1_Tick();

try 
{
    VideoCaptureManager manager = new VideoCaptureManager();
    FaceDetector cascader = new FaceDetector();
    FaceRecognitionEngine engine = new FaceRecognitionEngine();
    manager.CreateWindow();
    
    while (true) 
    {
        Mat frame = manager.GetFrame();
        
        if(CvInvoke.WaitKey(1) == 27) { manager.Release(); break; }

        System.Drawing.Rectangle[] faces = cascader.DetectFaces(frame);

        foreach (System.Drawing.Rectangle face in faces)
        {
            CvInvoke.Rectangle(frame, face, 
                new Bgr(System.Drawing.Color.Red).MCvScalar);

            Image<Bgr, byte> inputImage = frame.ToImage<Bgr, byte>();
            Image<Gray, byte> grayImageX = inputImage.Convert<Gray, byte>();

            //engine.RecognizeFacesInImage(grayImageX);

        }

        CvInvoke.Imshow(manager.WindowName, frame);
    }
}
catch (Exception ex) { System.Console.WriteLine(ex); }

/*
try
{
    VideoCapture capture = this.VideoCapture;
    string window_name = this.WindowName;
    CvInvoke.NamedWindow(window_name);

    while (true)
    {
        // Get capture by frame and desplay it into the test window.
        Mat frame = capture.QueryFrame();

        if (frame == null)
            break;

        //TODO Stops main loop, replace into 'fancyer shutdown'
        if (CvInvoke.WaitKey(1) == 27)
            break;

        Mat grayFrame = new Mat();
        CvInvoke.CvtColor(
            frame, grayFrame, ColorConversion.Bgr2Gray);

        System.Drawing.Rectangle[] faces = this.Cascade.DetectMultiScale(
            grayFrame, 1.4, 0);

        Image<Bgr, byte> inputImage = frame.ToImage<Bgr, byte>();
        Image<Gray, byte> grayImageX = inputImage.Convert<Gray, byte>();


        foreach (System.Drawing.Rectangle face in faces)
        {
            CvInvoke.Rectangle(frame, face, new Bgr(Color.Red).MCvScalar);

            System.Console.WriteLine(face + " gedectecteerd");
        }

        CvInvoke.Imshow(window_name, frame);
    }
}
catch (Exception ex)
{
    this.ExceptionHandeling(ex);
}*/