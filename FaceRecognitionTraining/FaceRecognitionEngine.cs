using Emgu.CV.Face;
using Emgu.CV;
using Emgu.CV.Structure;

public class FaceRecognitionEngine
{
    private EigenFaceRecognizer recognizer;
    private Dictionary<int, string> labelMap;

    public FaceRecognitionEngine(EigenFaceRecognizer recognizer, Dictionary<int, string> labelMap)
    {
        this.recognizer = recognizer;
        this.labelMap = labelMap;
    }

    public void RecognizeFacesInImage(Image<Gray, byte> image)
    {
        if (recognizer == null)
        {
            Console.WriteLine("Error: The recognizer is not set. Please set the recognizer before calling RecognizeFacesInImage.");
            return;
        }

        var result = recognizer.Predict(image.Resize(100, 100, Emgu.CV.CvEnum.Inter.Linear).Mat);

        int predictedLabel = result.Label;
        string predictedName = labelMap.ContainsKey(predictedLabel) ? labelMap[predictedLabel] : "Unknown";
        double distance = result.Distance;

        Console.WriteLine("Predicted Label: " + predictedName);
        Console.WriteLine("Distance: " + distance);
    }
}
