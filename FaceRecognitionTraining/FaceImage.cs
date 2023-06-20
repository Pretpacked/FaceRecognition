using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionTraining
{
    internal class FaceImage
    {
        private Image<Gray, byte> faceImage;
        private string[] fullFileName;

        public FaceImage(Image<Gray, byte> faceImage, string[] fullFileName)
        {
            this.faceImage = faceImage;
            this.fullFileName = fullFileName;
        }

        public Image<Gray, Byte> GetImage() { return this.faceImage.Resize(100, 100, Inter.Linear); }
        public string GetFullFileName() { return this.fullFileName[1]; }
        public int GetLabel() { return Convert.ToInt16(this.fullFileName[0]); }

    }
}
