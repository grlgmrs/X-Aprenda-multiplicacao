using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InfiniteModePointsManager
{
  private readonly TMP_Text PointsLabel;
  private int Points { get { return int.Parse(PointsLabel.text); } set { PointsLabel.text = value.ToString(); } }

  public InfiniteModePointsManager(TMP_Text pointsLabel)
  {
    PointsLabel = pointsLabel;
  }

  public void CalculatePoints(float elapsedTime, Action onCritical)
  {
    int points = Constants.InfiniteModeMaxPoints;

    elapsedTime = Math.Min(elapsedTime, Constants.InfiniteModeMaxResponseTime);

    if (elapsedTime > Constants.InfiniteModeResponseTimeTolerance)
    {
      float scorePercentage = Math.Max(
        1 - (elapsedTime - Constants.InfiniteModeResponseTimeTolerance) / (Constants.InfiniteModeMaxResponseTime - Constants.InfiniteModeResponseTimeTolerance),
        Constants.InfiniteModeMinPointsPercentage
      );
      points = Mathf.FloorToInt(points * scorePercentage);
    }
    else
      onCritical();

    Points += points;
  }
}