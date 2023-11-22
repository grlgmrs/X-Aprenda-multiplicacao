using System.IO;
using UnityEngine;
using System.Diagnostics;

public class ImageOpener : MonoBehaviour
{
    public Texture2D imageToOpen;

    public void OpenImageWithDefaultApp()
    {
        string tempPath = Path.Combine(Application.temporaryCachePath, "tempMultiplicationTable.png");
        SaveImage(tempPath, imageToOpen);
        Process.Start(tempPath);
    }

    private void SaveImage(string filePath, Texture2D image)
    {
        byte[] imageBytes = image.EncodeToPNG();
        File.WriteAllBytes(filePath, imageBytes);
    }
}
