using System;
using TMPro;
using UnityEngine;

public class InfiniteModeLoader : MonoBehaviour
{
  public GameObject buttonPrefab;
  public GameObject visorNumbers;
  private InfiniteModeGameManager GameManager;
  private InfiniteModeVisorManager VisorManager;
  private InfiniteModeButtonManager ButtonManager;

  void Start()
  {
    GameManager = new InfiniteModeGameManager();
    VisorManager = new InfiniteModeVisorManager(visorNumbers);
    ButtonManager = new InfiniteModeButtonManager(
      buttonPrefab,
      transform,
      GameManager.OnHit,
      GameManager.OnMiss
    );

    Reload();
  }

  private void Reload()
  {
    ButtonManager.Dispose();

    GameManager.GenerateOptions();
    VisorManager.SetAnswer(GameManager.Answer.ToString());
    ButtonManager.Initialize(
      Reload,
      GameManager.Options,
      GameManager.Answer
    );
  }
}
