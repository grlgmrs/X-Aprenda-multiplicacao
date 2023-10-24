using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonSpawner : MonoBehaviour
{
  public GameObject buttonPrefab;

  public GameObject visorNumbers;

  private readonly List<GameObject> instantiatedButtons = new();

  void Start()
  {
    LoadRound();
  }

  private void InstantiateButton(Coordinate coordinate, string text, UnityAction onClick)
  {
    GameObject buttonInstance = Instantiate(buttonPrefab, Helpers.GetButtonCoordinates(coordinate), Quaternion.identity);
    buttonInstance.transform.SetParent(transform);

    var label = buttonInstance.GetComponentInChildren<TMP_Text>();
    var button = buttonInstance.GetComponent<Button>();

    label.text = text;
    button.onClick.AddListener(onClick);

    instantiatedButtons.Add(buttonInstance);
  }

  private void CleanRound()
  {
    instantiatedButtons.ForEach(button => Destroy(button));
  }

  private void LoadRound()
  {
    InstantiateButton(Coordinate.TopRight, "Reload", () =>
    {
      Debug.Log("teste");
      CleanRound();
      LoadRound();
    });

    LoadOptions();
  }

  private void LoadOptions()
  {
    Coordinate[] coordinates = { Coordinate.TopCenter, Coordinate.MiddleCenter, Coordinate.MiddleRight, Coordinate.TopLeft, Coordinate.MiddleLeft, Coordinate.BottomCenter };

    var visorLabel = visorNumbers.GetComponent<TMP_Text>();
    var random = new System.Random();
    var answerLength = random.Next(2, 6);
    var answers = new Tuple<string, int>[answerLength];
    var answerChoosenIndex = random.Next(0, answerLength);

    // Generate options and answers
    for (var i = 0; i < answerLength; i++)
      answers[i] = Helpers.GenerateValue(10);

    // Choose an answer
    var answer = answers[answerChoosenIndex];

    // Instantiate buttons
    for (var i = 0; i < answerLength; i++)
    {
      var label = answers[i].Item1;
      var value = answers[i].Item2;

      InstantiateButton(coordinates[i], label, () => OnButtonClick(value, answer.Item2));
    }

    // Show choosen answer 
    visorLabel.text = answer.Item2.ToString();
  }

  private void OnButtonClick(int value, int rightAnswer)
  {
    Debug.Log("value");

    if (value == rightAnswer)
    {
      Debug.Log("venceu");
    }
    else
    {
      Debug.Log("perdeu");
    }
  }
}
