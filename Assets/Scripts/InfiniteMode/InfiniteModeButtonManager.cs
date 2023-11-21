using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class InfiniteModeButtonManager
{
  public Action OnHit;
  public Action OnMiss;
  private readonly Coordinate[] coordinates = {
    Coordinate.TopCenter,
    Coordinate.MiddleCenter,
    Coordinate.MiddleRight,
    Coordinate.MiddleLeft,
    Coordinate.BottomCenter,
    Coordinate.BottomRight,
    Coordinate.TopRight
  };
  private readonly Transform ParentTransform;
  private readonly GameObject ButtonPrefab;
  private readonly GameObject EmptyButtonPrefab;
  private readonly GameObject HomeButtonPrefab;
  private readonly GameObject HintButtonPrefab;
  private readonly List<GameObject> InstantiatedButtons = new();

  public InfiniteModeButtonManager(
    GameObject buttonPrefab,
    GameObject emptyButtonPrefab,
    GameObject homeButtonPrefab,
    GameObject hintButtonPrefab,
    Transform transform)
  {
    ButtonPrefab = buttonPrefab;
    EmptyButtonPrefab = emptyButtonPrefab;
    HomeButtonPrefab = homeButtonPrefab;
    HintButtonPrefab = hintButtonPrefab;
    ParentTransform = transform;
  }

  public void ClearButttons()
  {
    /// Remove todos os botões instanciados da memória
    InstantiatedButtons.ForEach(button => MonoBehaviour.Destroy(button));
    /// Limpa a lista de botões instanciados
    InstantiatedButtons.Clear();
  }

  public void Initialize(Tuple<string, int>[] options, int answer)
  {
    /// Botão para abrir a página de dicas, coloca ele na parte superior esquerda da tela
    InstantiateUniqueButton(Coordinate.TopLeft, HintButtonPrefab, () => SceneManager.LoadScene("Scenes/Hints", LoadSceneMode.Additive));
    /// Botão para voltar ao menu principal, coloca ele na parte inferior esquerda da tela e ao clicar chama a tela inicial
    InstantiateUniqueButton(Coordinate.BottomLeft, HomeButtonPrefab, () => SceneManager.LoadScene("Scenes/Home"));


    /// Gera os botões com as possíveis respostas
    foreach (var ((label, value), index) in options.WithIndex())
      InstantiateButton(coordinates[index], label, () => OnButtonClick(value, answer));

    /// Gera botões vazios e não clicáveis
    for (var index = options.Count(); index < coordinates.Count(); index++)
      InstantiateUniqueButton(coordinates[index], EmptyButtonPrefab);
  }

  public void InstantiateGameFinishedKeyboard()
  {
    InstantiateUniqueButton(Coordinate.TopLeft, EmptyButtonPrefab);
    InstantiateUniqueButton(Coordinate.BottomLeft, HomeButtonPrefab, () => SceneManager.LoadScene("Scenes/Home"));

    for (var index = 0; index < coordinates.Count(); index++)
      InstantiateUniqueButton(coordinates[index], EmptyButtonPrefab);
  }

  private void OnButtonClick(int value, int answer)
  {
    /// Verifica se o botão clicado tem a resposta correta, e se tiver, chama função de acerto. Se não, chama a função de erro
    if (value == answer)
      OnHit();
    else
      OnMiss();
  }

  private GameObject InstantiateUniqueButton(Coordinate coordinate, GameObject buttonPrefab, Action onClick = null)
  {
    /// Instancia o botão com base no prefab fornecido e na posição (x, y, z) do pai
    GameObject buttonInstance = MonoBehaviour.Instantiate(buttonPrefab, ParentTransform);
    buttonInstance.transform.localPosition = Helpers.GetButtonCoordinates(coordinate);
    /// Adiciona o botão à lista de instancias de botões para depois remover elas da memória
    InstantiatedButtons.Add(buttonInstance);

    var button = buttonInstance.GetComponent<Button>();
    /// Adiciona a ação de clique no botão
    button.onClick.AddListener(() => onClick());

    return buttonInstance;
  }

  private void InstantiateButton(Coordinate coordinate, string text, Action onClick)
  {
    var buttonInstance = InstantiateUniqueButton(coordinate, ButtonPrefab, onClick);
    var label = buttonInstance.GetComponentInChildren<TMP_Text>();

    /// Muda o texto do botão
    label.text = text;
  }
}
