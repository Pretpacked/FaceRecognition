using System;
using System.Runtime.CompilerServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using RabbitMQ.Client;
using Emgu.CV.Face;
using FaceRecognitionTraining;
using System.Drawing;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Presentation;
using Emgu.CV.Dai;
using Microsoft.VisualBasic.FileIO;
using Microting.eForm.Infrastructure.Data.Entities;
using System.IO;
using DocumentFormat.OpenXml.Vml;

namespace FaceRecognitionTraining
{
	public class VideoFeed
	{
        public VideoCapture VideoCapture { get; }
        public string WindowName { get; }

        private List<Image<Gray, byte>> FaceDatabase;
        private CascadeClassifier Cascade;
        private EigenFaceRecognizer Recognizer;

        private string MyDocuments = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments);

        public VideoFeed()
		{
            this.VideoCapture = new VideoCapture(0);
            this.WindowName = "window_name";

            this.Cascade = new CascadeClassifier(this.MyDocuments + "\\haarcascade_frontalface_alt.xml");

            this.FaceDatabase = this.LoadFaceDatabase();
        }

        public void timer1_Tick()
        {
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
            }
        }

        private List<Image<Gray, byte>> LoadFaceDatabase()
        {
            List<Image<Gray, byte>> faceImages = new List<Image<Gray, byte>>();

            try
            {
                string path = this.MyDocuments + "\\FacesDataSet";
                string[] files = Directory.GetFiles(
                    path, "*.jpg", System.IO.SearchOption.AllDirectories);

                foreach (string image in files)
                {
                    Image<Gray, byte> faceImage = new Image<Gray, byte>(image);
                    faceImages.Add(faceImage);
                    //Console.WriteLine(image);
                }
            }
            catch (Exception ex)
            {
                this.ExceptionHandeling(ex);
            }
            return faceImages;
        }

        private void ExceptionHandeling(Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
}
