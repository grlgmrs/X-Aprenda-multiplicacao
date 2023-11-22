using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMain : MonoBehaviour
{
  public void OnButtonPlayClick()
  {
    SceneManager.LoadScene("Scenes/InfiniteMode"); //(Scene que deverá ser aberta quando clicado)
  }
  public void OnButtonHintsClick()
  {
    SceneManager.LoadScene("Scenes/Hints", LoadSceneMode.Additive); //(Scene que deverá ser aberta quando clicado)
  }
  public void OnButtonMultiplicationClick()
  {
    OpenMultiplicationTable();
  }
  public void OnButtonCreditsClick()
  {
    SceneManager.LoadScene("Scenes/Credits"); //(Scene que deverá ser aberta quando clicado)
  }
  public void Quit()
  {
    Application.Quit(); //fecha jogo
  }

  private void OpenMultiplicationTable()
  {
    Texture2D multiplicationTableImage = Resources.Load<Texture2D>("tabuada");

    string tempPath = Path.Combine(Application.temporaryCachePath, "tempMultiplicationTable.png");
    SaveImage(tempPath, multiplicationTableImage);
    Process.Start(tempPath);
  }

  private void SaveImage(string filePath, Texture2D image)
  {
    byte[] imageBytes = image.EncodeToPNG();
    File.WriteAllBytes(filePath, imageBytes);
  }
}