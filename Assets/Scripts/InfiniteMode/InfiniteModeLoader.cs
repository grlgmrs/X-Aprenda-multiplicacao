using UnityEngine;

public class InfiniteModeLoader : MonoBehaviour
{
  public GameObject ButtonPrefab;
  public GameObject HintButtonPrefab;
  public GameObject HomeButtonPrefab;
  public GameObject EmptyButtonPrefab;
  public GameObject HealthPointPrefab;
  public GameObject VisorNumbers;
  public GameObject LifeBar;
  private InfiniteModeGameManager GameManager;
  private InfiniteModeVisorManager VisorManager;
  private InfiniteModeButtonManager ButtonManager;

  private InfiniteModeLifeBarManager LifeBarManager;

  void Start()
  {
    LifeBarManager = new InfiniteModeLifeBarManager(LifeBar, HealthPointPrefab);
    ButtonManager = new InfiniteModeButtonManager(
      ButtonPrefab,
      EmptyButtonPrefab,
      HomeButtonPrefab,
      HintButtonPrefab,
      transform
    );
    VisorManager = new InfiniteModeVisorManager(VisorNumbers);
    GameManager = new InfiniteModeGameManager(
      LifeBarManager,
      VisorManager,
      ButtonManager
    );
  }
}
