using System;

public class InfiniteModeGameManager
{
  public int Answer { get; private set; }
  public Tuple<string, int>[] Options { get; private set; }

  private readonly InfiniteModeLifeBarManager LifeBarManager;
  private readonly InfiniteModeVisorManager VisorManager;
  private readonly InfiniteModeButtonManager ButtonManager;
  private readonly InfiniteModeTimeManager TimeManager;
  private readonly InfiniteModePointsManager PointsManager;

  public InfiniteModeGameManager(
    InfiniteModeLifeBarManager lifeBarManager,
    InfiniteModeVisorManager visorManager,
    InfiniteModeButtonManager buttonManager,
    InfiniteModeTimeManager timeManager,
    InfiniteModePointsManager pointsManager
  )
  {
    LifeBarManager = lifeBarManager;
    VisorManager = visorManager;
    ButtonManager = buttonManager;
    TimeManager = timeManager;
    PointsManager = pointsManager;

    ButtonManager.OnHit = OnHit;
    ButtonManager.OnMiss = OnMiss;
    TimeManager.OnTimesUp = OnTimesUp;

    Reload();
  }

  private void OnHit()
  {
    PointsManager.CalculatePoints(
      TimeManager.GetElapsedTimeSincePhaseStarted(),
      () =>
      {
        TimeManager.AddTime(3f);
        LifeBarManager.AddHealthPoints(1);
      }
    );
    NextPhase();
  }

  private void OnMiss()
  {
    LifeBarManager.RemoveHealthPoints(3);

    if (LifeBarManager.CurrentHealth == 0)
      Lose();
  }


  public void GenerateOptions()
  {
    var random = new System.Random();
    var answerLength = random.Next(Constants.InfiniteModeMinOptions, Constants.InfiniteModeMaxOptions);

    Options = new Tuple<string, int>[answerLength];

    for (var i = 0; i < answerLength; i++)
      Options[i] = Helpers.GenerateValue(10);

    Answer = Options[random.Next(0, answerLength)].Item2;
  }

  #region Game workflow

  private void Reload()
  {
    LifeBarManager.ResetCurrentHealth();
    NextPhase();
  }

  private void NextPhase()
  {
    TimeManager.OnPhaseStart();
    ButtonManager.ClearButttons();
    GenerateOptions();

    VisorManager.SetAnswer(Answer.ToString());
    ButtonManager.Initialize(
      Options,
      Answer
    );
  }

  private void Lose()
  {
    ButtonManager.ClearButttons();
    ButtonManager.InstantiateGameFinishedKeyboard();
    TimeManager.SetTimerActive(false);
    VisorManager.SetAnswer("FIM");
  }

  private void OnTimesUp()
  {
    ButtonManager.ClearButttons();
    ButtonManager.InstantiateGameFinishedKeyboard();
    VisorManager.SetAnswer("FIM");
  }

  #endregion
}