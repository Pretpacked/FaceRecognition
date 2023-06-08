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

namespace FaceRecognitionTraining
{
	public class VideoFeed
	{
        public VideoCapture VideoCapture { get; }
        public string Name { get; }

        public List<List<string>> FaceDatabase { get; set; }

        public CascadeClassifier Cascade;
        public EigenFaceRecognizer Recognizer;

        private string MyDocuments = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments);

        public VideoFeed()
		{
            this.VideoCapture = new VideoCapture(0);
            this.Name = "window_name";

            this.Cascade = new CascadeClassifier(this.MyDocuments + "\\haarcascade_frontalface_alt.xml");
        }

        public void timer1_Tick()
        {
            try
            {
                VideoCapture capture = this.VideoCapture;
                string window_name = this.Name;
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

                    Rectangle[] faces = this.Cascade.DetectMultiScale(
                        grayFrame, 1.4, 0);

                    Image<Bgr, byte> inputImage = frame.ToImage<Bgr, byte>();
                    Image<Gray, byte> grayImageX = inputImage.Convert<Gray, byte>();


                    foreach (Rectangle face in faces)
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

            // Load images from face database 
            string path = this.MyDocuments + "\\FacesDataSet";
            string[] files = Directory.GetFiles(
                path, "*.jpg", System.IO.SearchOption.AllDirectories);

            foreach (string personDir in files)
            {
                string personName = Path.GetFileName(personDir);

                string[] imageFiles = Directory.GetFiles(personDir);
                foreach (string imageFile in imageFiles)
                {
                    Image<Gray, byte> faceImage = new Image<Gray, byte>(imageFile);
                    faceImages.Add(faceImage);
                }
            }

            return faceImages;
        }
        /*
        public void imagesFromDir() {
            try
            {
                string path = this.MyDocuments + "\\FacesDataSet";
                string[] files = Directory.GetFiles(
                    path, "*.jpg", System.IO.SearchOption.AllDirectories);
                int i = 0;

                var faceDatabase = new List<List<string>>();

                foreach (var file in files)
                {
                    DirectoryInfo directory = Directory.GetParent(file);

                    faceDatabase.Add(
                        new List<string>());
                    faceDatabase[i].Add(
                        directory.Name.ToString());
                    faceDatabase[i].Add(
                        file.ToString());

                    i++;
                }
                this.FaceDatabase = faceDatabase;

                // testing
                faceDatabase[3].ForEach(Console.WriteLine);
            }
            catch (Exception ex)
            {
                this.ExceptionHandeling(ex);
            }
        }*/

        private void ExceptionHandeling(Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
}
