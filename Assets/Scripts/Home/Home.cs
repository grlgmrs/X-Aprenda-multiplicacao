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
        SceneManager.LoadScene("Scenes/InfiniteMode"); //(Scene que deverá ser aberta quando clicado)
    }
    public void OnButtonHintsClick()
    {
        SceneManager.LoadScene("Scenes/Hints", LoadSceneMode.Additive); //(Scene que deverá ser aberta quando clicado)
    }
    public void OnButtonMultiplicationClick()
    {
        SceneManager.LoadScene(""); //(Scene que deverá ser aberta quando clicado)
    }
    public void OnButtonCreditsClick()
    {
        SceneManager.LoadScene(""); //(Scene que deverá ser aberta quando clicado)
    }
    public void Quit()
    {
        Application.Quit(); //fecha jogo
    }

}
