using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsLoader : MonoBehaviour
{
  public GameObject Background; //componente onde as imagens serão carregadas
  public GameObject LeftButton; //controlar esquerdo
  public GameObject RightButton; //controlar direito
  public GameObject ReturnButton; //botão de retorno
  public List<Sprite> Hints; //sprite -> imagem
  private int ImageIndex = 0;

  void Start()
  {
    var leftButton = LeftButton.GetComponent<Button>(); //
    var rightButton = RightButton.GetComponent<Button>();

    leftButton.onClick.AddListener(PreviousImage);
    rightButton.onClick.AddListener(NextImage);
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
    if (--ImageIndex < 0) ImageIndex = Hints.Count;//count -> conta quantas imagens temos (vetores)
    RefreshCurrentImage();
  }

  public void OnButtonReturnClick()
  {
    Application.LoadLevel("Scenes/Home");
  }
}
