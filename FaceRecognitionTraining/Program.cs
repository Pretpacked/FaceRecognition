using FaceRecognitionTraining;

Main();

static void Main()
{
    AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

    Console.WriteLine("test");

    VideoFeed x = new VideoFeed();
}

static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
{
    if (e.ExceptionObject is System.IO.FileLoadException fileLoadException)
    {
        // Handle the FileLoadException here
        Console.WriteLine("FileLoadException occurred: " + fileLoadException.Message);
        // Additional error handling or logging can be performed
    }
    else
    {
        // Handle other unhandled exceptions here
        Console.WriteLine("Unhandled exception occurred: " + e.ExceptionObject.ToString());
    }

    // Terminate the application or perform any necessary cleanup
    Environment.Exit(1);
}
