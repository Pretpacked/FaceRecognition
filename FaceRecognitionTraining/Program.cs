using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Face;
using Emgu.CV.CvEnum;
using FaceRecognitionTraining;
using System.Drawing;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Presentation;
using Emgu.CV.Dai;

VideoFeed x = new VideoFeed();


x.timer1_Tick();
x.imagesFromDir();