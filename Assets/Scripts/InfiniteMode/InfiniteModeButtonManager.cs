using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class InfiniteModeButtonManager
{
  private readonly Coordinate[] coordinates = { Coordinate.TopCenter, Coordinate.MiddleCenter, Coordinate.MiddleRight, Coordinate.TopLeft, Coordinate.MiddleLeft, Coordinate.BottomCenter };
  private readonly Transform ParentTransform;
  private readonly GameObject ButtonPrefab;
  private readonly List<GameObject> InstantiatedButtons = new();
  private readonly Action OnHit;
  private readonly Action OnMiss;

  public InfiniteModeButtonManager(GameObject buttonPrefab, Transform transform, Action onHit, Action onMiss)
  {
    ButtonPrefab = buttonPrefab;
    ParentTransform = transform;
    OnHit = onHit;
    OnMiss = onMiss;
  }

  public void Dispose()
  {
    InstantiatedButtons.ForEach(button => MonoBehaviour.Destroy(button));
  }

  public void Initialize(Action onReloadClick, Tuple<string, int>[] options, int answer)
  {
    InstantiateButton(Coordinate.TopRight, "Reload", onReloadClick);

    foreach (var ((label, value), index) in options.WithIndex())
      InstantiateButton(coordinates[index], label, () => OnButtonClick(value, answer));
  }

  private void OnButtonClick(int value, int answer)
  {
    if (value == answer)
      OnHit();
    else
      OnMiss();
  }

  private void InstantiateButton(Coordinate coordinate, string text, Action onClick)
  {
    GameObject buttonInstance = MonoBehaviour.Instantiate(ButtonPrefab, Helpers.GetButtonCoordinates(coordinate), Quaternion.identity);
    buttonInstance.transform.SetParent(ParentTransform);

    var label = buttonInstance.GetComponentInChildren<TMP_Text>();
    var button = buttonInstance.GetComponent<Button>();

    label.text = text;
    button.onClick.AddListener(() => onClick());

    InstantiatedButtons.Add(buttonInstance);
  }
}
