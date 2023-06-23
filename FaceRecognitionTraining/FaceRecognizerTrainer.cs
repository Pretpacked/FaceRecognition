using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Util;
using Emgu.CV;
using Emgu.CV.Structure;

public class FaceRecognizerTrainer
{
    public List<FaceImage> faceDatabase;
    private EigenFaceRecognizer recognizer;
    private FaceDetector detector;

    public FaceRecognizerTrainer(FaceDetector cascader)
    {
        this.faceDatabase = new List<FaceImage>();
        this.detector = cascader;
        this.recognizer = new EigenFaceRecognizer();
        this.LoadFaceDatabase();
    }

    public EigenFaceRecognizer GetRecognizer()
    {
        return recognizer;
    }

    private void LoadFaceDatabase()
    {
        faceDatabase.Clear();

        string path = Path.Combine(this.detector.GetDocumentsLocation(), "FaceDatabase");
        string[] files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories);

        foreach (string image in files)
        {
            string[] fullFileName = Path.GetFileNameWithoutExtension(image).Split('_');

            Image<Gray, byte> faceImage = new Image<Gray, byte>(image);
            FaceImage x = new FaceImage(faceImage, fullFileName);
            this.faceDatabase.Add(x);
        }
    }

    public void TrainFaceRecognizer()
    {
        List<Mat> faces = new List<Mat>();
        List<int> labels = new List<int>();
        Dictionary<string, int> labelMap = new Dictionary<string, int>();

        int currentLabel = 0;

        foreach (FaceImage faceImage in this.faceDatabase)
        {
            Mat faceMat = faceImage.GetImage().Mat;
            int label = faceImage.GetLabel();

            if (!labelMap.ContainsKey(label.ToString()))
            {
                labelMap[label.ToString()] = currentLabel;
                currentLabel++;
            }

            faces.Add(faceMat);
            labels.Add(labelMap[label.ToString()]);
        }

        // Convert faces and labels to the appropriate input types
        var facesInput = new VectorOfMat(faces.ToArray());
        var labelsInput = new Mat(labels.Count, 1, DepthType.Cv32S, 1);
        labelsInput.SetTo(labels.ToArray());

        // Train face recognizer
        recognizer.Train(facesInput, labelsInput);
    }
}