﻿using Emgu.CV;
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

try 
{
    VideoCaptureManager manager = new VideoCaptureManager();
    FaceDetector cascader = new FaceDetector();
    FaceRecognizerTrainer trainer = new FaceRecognizerTrainer(cascader);
    FaceRecognitionEngine engine = new FaceRecognitionEngine(
        trainer.GetRecognizer());

    trainer.TrainFaceRecognizer();

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
            inputImage.Resize(100 ,100, Inter.Linear);

            Image<Gray, byte> grayImageX = inputImage.Convert<Gray, byte>();

            engine.RecognizeFacesInImage(grayImageX);

        
        }
        CvInvoke.Imshow(manager.GetWindowName(), frame);
    }
}
catch (Exception ex) { System.Console.WriteLine(ex); }