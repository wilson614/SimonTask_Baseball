using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FunCryLoader : MonoBehaviour
{
    [Header("讀取介面群組")]
    public CanvasGroup canvasGroup;
    [Header("讀取條")]
    public Slider loadingSlider;
    [Header("進度文字")]
    public Text loadingText;
    [Header("讀取速度")]
    private float loadingSpeed = 5;

    private float targetValue;

    private AsyncOperation operation;

    [Header("讀取狀態")]
    public bool run;

    void Start()
    {
        loadingSlider.value = 0.0f;
    }

    void Update()
    {
        if (run)
        {
            canvasGroup.alpha = 1;
            targetValue = operation.progress;

            if (operation.progress >= 0.9f)
            {
                targetValue = 1.0f;
            }

            if (targetValue != loadingSlider.value)
            {
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
                if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
                {
                    loadingSlider.value = targetValue;
                }
            }

            loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

            if ((int)(loadingSlider.value * 100) == 100)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
    public void load(int index)
    {
        StartCoroutine(AsyncLoading(index));
        run = true;
    }

    IEnumerator AsyncLoading(int index)
    {
        operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;

        yield return operation;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
