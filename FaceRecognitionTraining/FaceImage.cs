using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceRecognitionTraining
{
    public class FaceImage
    {
        private Image<Gray, byte> faceImage;
        private string parentFolderName;
        private int label;

        public FaceImage(Image<Gray, byte> faceImage, string parentFolderName, int label)
        {
            this.faceImage = faceImage;
            this.parentFolderName = parentFolderName;
            this.label = label;
        }

        public Image<Gray, byte> GetImage() { return this.faceImage.Resize(100, 100, Inter.Linear); }
        public string GetParentFolderName() { return this.parentFolderName; }
        public int GetLabel() { return this.label; }
    }
}
