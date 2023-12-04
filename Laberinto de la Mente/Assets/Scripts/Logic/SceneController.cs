using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour
{
    public string sceneLoadName;
    public TextMeshProUGUI textProgress;
    public Slider sliderProgress;
    public float currentPercent;

    public void LoadSceneButton()
    {
        StartCoroutine(LoadScene(sceneLoadName));
    }

    public IEnumerator LoadScene(string nameToLoad)
    {
        textProgress.text = "Cargando.. 00%";
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(nameToLoad);

        while(!loadAsync.isDone)
        {
            // 0.9 -> 100
            // 0.9? -> 0.9 * 100 / 0.9
            currentPercent = loadAsync.progress * 100 / 0.9f;
            textProgress.text = "Cargando.. " + currentPercent.ToString("00") + "%";
            yield return null;
        }
    }

    private void Update()
    {
        sliderProgress.value = Mathf.MoveTowards(sliderProgress.value, currentPercent, 10 * Time.deltaTime);
    }
}
