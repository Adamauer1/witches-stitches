using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject storyScreen1;
    [SerializeField] private GameObject storyScreen2;
    [SerializeField] private GameObject storyScreen3;
    [SerializeField] private GameObject storyScreen4;

    private void Awake()
    {
        storyScreen1.SetActive(false);
        storyScreen2.SetActive(false);
        storyScreen3.SetActive(false);
        storyScreen4.SetActive(false);
    }

    public void HandlePlayButton () {
        // continue to story opener
        mainMenu.SetActive(false);
        storyScreen1.SetActive(true);

    }

    public void HandleNext()
    {
        storyScreen1.SetActive(false);
        storyScreen2.SetActive(true);
    }

    public void HandleNext2()
    {
        storyScreen2.SetActive(false);
        storyScreen3.SetActive(true);
    }

    public void HandleNext3()
    {
        storyScreen3.SetActive(false);
        storyScreen4.SetActive(true);
    }

    public void HandleExitButton (){
        Application.Quit();
    }

    public void HandleStartButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
