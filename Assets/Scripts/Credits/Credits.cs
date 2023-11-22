using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMain : MonoBehaviour
{
  public void OnButtonReturnClick()
  {
    SceneManager.LoadSceneAsync("Home");
  }
}
