using Emgu.CV;

public class VideoCaptureManager
{
    private VideoCapture videoCapture;
    private string windowName;

    public VideoCaptureManager()
    {
        this.videoCapture = new VideoCapture(0);
        this.windowName = "test window";
    }

    public Mat GetFrame()
    {
        Mat frame = this.videoCapture.QueryFrame();
        if (frame == null)
        {
            this.Release();
        }
        return frame;
    }

    public string GetWindowName()
    {
        return this.windowName;
    }

    public void CreateWindow()
    {
        CvInvoke.NamedWindow(this.windowName);
    }

    public void Release()
    {
        videoCapture.Dispose();
    }
}