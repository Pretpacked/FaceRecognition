using Emgu.CV;
using Emgu.CV.Structure;

public class FaceImage
{
    private Image<Gray, byte> faceImage;
    private string[] fullFileName;

    public FaceImage(Image<Gray, byte> faceImage, string[] fullFileName)
    {
        this.faceImage = faceImage;
        this.fullFileName = fullFileName;
    }

    public Image<Gray, byte> GetImage()
    {
        return this.faceImage.Resize(100, 100, Emgu.CV.CvEnum.Inter.Linear);
    }

    public string GetFullFileName()
    {
        return this.fullFileName[0];
    }


    public int GetLabel()
    {
        if (int.TryParse(this.fullFileName[0], out int label))
        {
            return label;
        }
        else
        {
            // Handle parsing error, return a default label
            return -1;
        }
    }
}