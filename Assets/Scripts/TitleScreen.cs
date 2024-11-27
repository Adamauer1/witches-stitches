using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject storyScreen;
    public void HandlePlayButton () {
        // continue to story opener
        mainMenu.SetActive(false);
        storyScreen.SetActive(true);

    }

    public void HandleExitButton (){
        Application.Quit();
    }

    public void HandleStartButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
