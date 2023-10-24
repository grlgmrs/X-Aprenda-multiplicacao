using TMPro;
using UnityEngine;

public class InfiniteModeVisorManager
{
  private readonly GameObject Visor;
  private TMP_Text Label { get { return Visor.GetComponent<TMP_Text>(); } }

  public InfiniteModeVisorManager(GameObject visor)
  {
    Visor = visor;
  }

  public void SetAnswer(string text)
  {
    Label.text = text;
  }
}
