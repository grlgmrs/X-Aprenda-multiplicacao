using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HomeMain : MonoBehaviour
{
    //[SerializeField] private GameObject Home;
    //[SerializeField] private GameObject HomeButtons;
    //[SerializeField] private GameObject PlayButton;
    //[SerializeField] private GameObject HinstButton;
    //[SerializeField] private GameObject MultiplicationButton;
    //[SerializeField] private GameObject CreditsButton;

    public void OnButtonPlayClick()
    {
        Application.LoadLevel("Scenes/InfiniteMode"); //(Scene que deverá ser aberta quando clicado)
    }
    public void OnButtonHintsClick()
    {
        Application.LoadLevel("Scenes/Hints"); //(Scene que deverá ser aberta quando clicado)
    }
    public void OnButtonMultiplicationClick()
    {
        Application.LoadLevel(""); //(Scene que deverá ser aberta quando clicado)
    }
    public void OnButtonCreditsClick()
    {
        Application.LoadLevel(""); //(Scene que deverá ser aberta quando clicado)
    }
    public void Quit()
    {
        Application.Quit(); //fecha jogo
    }

}
