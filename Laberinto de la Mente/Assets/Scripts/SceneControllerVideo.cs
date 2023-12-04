using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneControllerVideo : MonoBehaviour
{
    public string sceneLoadName;
    public TextMeshProUGUI textProgress;
    public Slider sliderProgress;
    public float currentPercent;
    public VideoPlayer videoPlayer; // Añade referencia al VideoPlayer

    private void Start()
    {
        // Asegúrate de que videoPlayer está asignado en el Inspector
        videoPlayer.loopPointReached += OnVideoEnded;
    }

    public void LoadSceneButton()
    {
        StartCoroutine(LoadScene(sceneLoadName));
    }

    public IEnumerator LoadScene(string nameToLoad)
    {
        textProgress.text = "Cargando.. 00%";
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(nameToLoad);

        while (!loadAsync.isDone)
        {
            currentPercent = loadAsync.progress * 100 / 0.9f;
            textProgress.text = "Cargando.. " + currentPercent.ToString("00") + "%";
            yield return null;
        }
    }

    private void Update()
    {
        sliderProgress.value = Mathf.MoveTowards(sliderProgress.value, currentPercent, 10 * Time.deltaTime);
    }

    // Método llamado cuando el video ha terminado
    private void OnVideoEnded(VideoPlayer vp)
    {
        // Llamamos al método LoadSceneButton para cargar la escena
        LoadSceneButton();
    }
}
