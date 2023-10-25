using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class InfiniteModeButtonManager
{
  public Action OnHit;
  public Action OnMiss;
  private readonly Coordinate[] coordinates = { Coordinate.TopCenter, Coordinate.MiddleCenter, Coordinate.MiddleRight, Coordinate.TopLeft, Coordinate.MiddleLeft, Coordinate.BottomCenter };
  private readonly Transform ParentTransform;
  private readonly GameObject ButtonPrefab;
  private readonly List<GameObject> InstantiatedButtons = new();

  public InfiniteModeButtonManager(GameObject buttonPrefab, Transform transform)
  {
    ButtonPrefab = buttonPrefab;
    ParentTransform = transform;
  }

  public void ClearButttons()
  {
    InstantiatedButtons.ForEach(button => MonoBehaviour.Destroy(button));
    InstantiatedButtons.Clear();
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
    GameObject buttonInstance = MonoBehaviour.Instantiate(ButtonPrefab, ParentTransform);
    buttonInstance.transform.localPosition = Helpers.GetButtonCoordinates(coordinate);

    var label = buttonInstance.GetComponentInChildren<TMP_Text>();
    var button = buttonInstance.GetComponent<Button>();

    label.text = text;
    button.onClick.AddListener(() => onClick());

    InstantiatedButtons.Add(buttonInstance);
  }
}
