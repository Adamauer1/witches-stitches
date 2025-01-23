using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject storyScreen1;
    [SerializeField] private GameObject storyScreen2;
    [SerializeField] private Image background;
    [SerializeField] private GameObject storyScreen3;
    // [SerializeField] private GameObject storyScreen5;
    [SerializeField] private float fadeTime = 1f;


    private void Awake()
    {
        // background.SetActive(true);
        storyScreen1.SetActive(false);
        storyScreen2.SetActive(false);
        storyScreen3.SetActive(false);
        // storyScreen5.SetActive(false);
        // Invoke("DisplayText1", 0.5f);
        // Debug.Log("Test");
    }

    private void Start()
    {
        Invoke("DisplayText1", 0.5f);
        Debug.Log("Test");
    }

    public void HandleNext()
    {
        storyScreen1.SetActive(false);
        Invoke("DisplayText2", 0.5f);
    }

    private void DisplayText1()
    {
        storyScreen1.SetActive(true);
    }

    private void DisplayText2()
    {
        storyScreen2.SetActive(true);
    }

    public void HandleNext2()
    {
        storyScreen2.SetActive(false);
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        float time = 0f;
        Color color = background.color;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float alpha = fadeTime - Mathf.Clamp01(time / fadeTime);
            background.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        background.color = new Color(color.r, color.g, color.b, 0f);
        DisplayText3();
    }

    private void DisplayText3()
    {
        storyScreen3.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
