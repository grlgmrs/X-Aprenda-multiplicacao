using System;
using UnityEngine;

public class InfiniteModeGameManager
{
  public int Answer { get; private set; }
  public Tuple<string, int>[] Options { get; private set; }

  private readonly InfiniteModeLifeBarManager LifeBarManager;
  private readonly InfiniteModeVisorManager VisorManager;
  private readonly InfiniteModeButtonManager ButtonManager;

  public InfiniteModeGameManager(
    InfiniteModeLifeBarManager lifeBarManager,
    InfiniteModeVisorManager visorManager,
    InfiniteModeButtonManager buttonManager
  )
  {
    LifeBarManager = lifeBarManager;
    VisorManager = visorManager;
    ButtonManager = buttonManager;

    ButtonManager.OnHit = OnHit;
    ButtonManager.OnMiss = OnMiss;

    Reload();
  }

  private void OnHit()
  {
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
    ButtonManager.ClearButttons();
    LifeBarManager.ResetCurrentHealth();
    GenerateOptions();

    VisorManager.SetAnswer(Answer.ToString());
    ButtonManager.Initialize(
      Reload,
      Options,
      Answer
    );
  }

  private void NextPhase()
  {
    ButtonManager.ClearButttons();
    GenerateOptions();

    VisorManager.SetAnswer(Answer.ToString());
    ButtonManager.Initialize(
      Reload,
      Options,
      Answer
    );
  }

  private void Lose()
  {
    ButtonManager.ClearButttons();
    VisorManager.SetAnswer("You lose");
  }

  #endregion
}