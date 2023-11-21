using System;
using System.Timers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfiniteModeTimeManager
{
  public Action OnTimesUp;
  private float TimeLeft = 60;
  private float PhaseStartTime = 0;
  private readonly TMP_Text Countdown;
  private bool TimerActive = true;

  public InfiniteModeTimeManager(TMP_Text countdown)
  {
    Countdown = countdown;
  }

  public void Update()
  {
    if (SceneManager.sceneCount == 1 && TimerActive)
    {
      if (TimeLeft > 0)
        UpdateTime();
      else
      {
        TimerActive = false;
        OnTimesUp();
      }
    }
  }

  public void OnPhaseStart()
  {
    PhaseStartTime = TimeLeft;
  }

  public float GetElapsedTimeSincePhaseStarted()
  {
    return PhaseStartTime - TimeLeft;
  }

  public void AddTime(float time)
  {
    TimeLeft += time;
  }

  public void SetTimerActive(bool timerActive)
  {
    TimerActive = timerActive;
  }

  private void UpdateTime()
  {
    TimeLeft = Math.Max(0, TimeLeft - Time.deltaTime);

    float minutes = Mathf.FloorToInt(TimeLeft / 60);
    float seconds = Mathf.FloorToInt(TimeLeft % 60);

    Countdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
  }
}