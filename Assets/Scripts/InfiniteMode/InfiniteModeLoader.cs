using TMPro;
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
  public GameObject Countdown;
  public GameObject Points;
  private InfiniteModeVisorManager VisorManager;
  private InfiniteModeButtonManager ButtonManager;
  private InfiniteModeLifeBarManager LifeBarManager;
  private InfiniteModeTimeManager TimeManager;
  private InfiniteModePointsManager PointsManager;

  void Start()
  {
    TimeManager = new InfiniteModeTimeManager(Countdown.GetComponent<TMP_Text>());
    PointsManager = new InfiniteModePointsManager(Points.GetComponent<TMP_Text>());
    LifeBarManager = new InfiniteModeLifeBarManager(LifeBar, HealthPointPrefab);
    ButtonManager = new InfiniteModeButtonManager(
      ButtonPrefab,
      EmptyButtonPrefab,
      HomeButtonPrefab,
      HintButtonPrefab,
      transform
    );
    VisorManager = new InfiniteModeVisorManager(VisorNumbers);

    new InfiniteModeGameManager(
      LifeBarManager,
      VisorManager,
      ButtonManager,
      TimeManager,
      PointsManager
    );
  }

  void Update()
  {
    TimeManager.Update();
  }
}
