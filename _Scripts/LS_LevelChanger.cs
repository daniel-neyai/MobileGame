using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LS_LevelChanger : MonoBehaviour
{
    public static LS_LevelChanger instance;

    public GameObject loadingScreen;

    public Slider slider;

    public Text progressText;

    public Animator animator;

    private int levelToLoad;

    private void Awake()
    {
        LS_GameManager.instance.disMusic = false;
    }

    private void Update()
    {
        if (levelToLoad == 1)
        {
            FindObjectOfType<LS_AudioManager>().Play("DeathMusic");
        }
    }
    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<LS_AudioManager>().Play("LoadingMusic");
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        //   SceneManager.LoadScene(levelToLoad);
        StartCoroutine(LoadAsynchronously(levelToLoad));
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);
        FindObjectOfType<LS_AudioManager>().Stop("Background Music");
    }

    IEnumerator LoadAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
