using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HintsLoader : MonoBehaviour
{
  public GameObject Background; //componente onde as imagens serão carregadas
  public GameObject LeftButton; //controlar esquerdo
  public GameObject RightButton; //controlar direito
  public GameObject ReturnButton; //botão de retorno
  public GameObject HintsImage;
  public List<Sprite> Hints; //sprite -> imagem
  private int ImageIndex = 0;

  void Start()
  {
    var leftButton = LeftButton.GetComponent<Button>(); //
    var rightButton = RightButton.GetComponent<Button>();
    var returnButton = ReturnButton.GetComponent<Button>();

    leftButton.onClick.AddListener(PreviousImage);
    rightButton.onClick.AddListener(NextImage);
    returnButton.onClick.AddListener(ReturnMenu);

    StartCoroutine(HintsFadeOut());
  }

  private void RefreshCurrentImage()
  {
    var image = Background.GetComponent<Image>(); //cria variável e atribui o componente de imagem
    image.sprite = Hints[ImageIndex]; //atribui o arquivo imagem na imagem componente "Background"
  }

  private void NextImage()
  {
    if (++ImageIndex >= Hints.Count) ImageIndex = 0;
    RefreshCurrentImage();
  }
  private void PreviousImage()
  {
    if (--ImageIndex < 0) ImageIndex = Hints.Count - 1;//count -> conta quantas imagens temos (vetores)
    RefreshCurrentImage();
  }

  private void ReturnMenu()
  {
    SceneManager.UnloadSceneAsync("Hints");
  }

  private IEnumerator HintsFadeOut()
  {
    yield return new WaitForSeconds(Constants.HintsHomeWaitUntilFadeOut);

    if (HintsImage.TryGetComponent<Image>(out var image))
    {
      float startAlpha = image.color.a;
      float rate = 1.0f / Constants.HintsHomeFadeOut;

      for (float i = 0; i < 1.0; i += Time.deltaTime * rate)
      {
        Color color = image.color;
        color.a = Mathf.Lerp(startAlpha, 0, i);
        image.color = color;
        yield return null;
      }

      Destroy(HintsImage);
    }
  }
}
