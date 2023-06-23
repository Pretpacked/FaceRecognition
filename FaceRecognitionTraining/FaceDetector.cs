using Emgu.CV.CvEnum;
using Emgu.CV;

public class FaceDetector
{
    private CascadeClassifier cascade;
    private string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    public FaceDetector()
    {
        this.cascade = new CascadeClassifier(Path.Combine(this.myDocuments, "haarcascade_frontalface_alt.xml"));
    }

    public string GetDocumentsLocation()
    {
        return this.myDocuments;
    }

    public System.Drawing.Rectangle[] DetectFaces(Mat frame)
    {
        Mat grayFrame = new Mat();
        CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);

        double scaleFactor = 1.1;
        int minNeighbors = 3;

        return cascade.DetectMultiScale(grayFrame, scaleFactor, minNeighbors);
    }
}