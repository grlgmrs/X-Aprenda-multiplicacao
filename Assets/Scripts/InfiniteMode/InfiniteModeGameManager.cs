using System;
using UnityEngine;

public class InfiniteModeGameManager
{
  public int Answer { get; private set; }
  public Tuple<string, int>[] Options { get; private set; }

  public void OnHit()
  {
    Debug.Log("Congratz!!");
  }

  public void OnMiss()
  {
    Debug.Log("Fail.");
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
}
