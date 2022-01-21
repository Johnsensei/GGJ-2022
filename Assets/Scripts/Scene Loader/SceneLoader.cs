using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Singleton;

    [Range(0.01f, 5)]
    public float FadeSpeed = 1;
    [Range(0.01f, 5)]
    public float FadeDelayAfterLoad = 1;

    public GameObject[] Faders;
    public GameObject SceneLoaderCanvas;

    public void Awake()
    {
        if (Singleton != null)
        {
            if (Singleton != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
            Singleton = this;

        if(Faders != null && Faders.Length > 0)
        {
            foreach(var fader in Faders)
            {
                fader.GetComponent<IFader>().SetFadeSpeed(FadeSpeed);
            }
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.UnloadSceneAsync("Loading Screen");
    }


    public static void LoadScene(int sceneIndex)
    {
        if (Singleton == null)
        {
            SceneManager.LoadSceneAsync("Loading Screen", LoadSceneMode.Single).completed += (asyncOperation) => {
                Singleton = FindObjectOfType<SceneLoader>();
                Singleton.FadeAndLoadScene(sceneIndex);
            };
            return;
        }

        Singleton.FadeAndLoadScene(sceneIndex);
    }


    void FadeAndLoadScene(int sceneIndex)
    {
        SceneLoaderCanvas.SetActive(true);
        SceneLoaderCanvas.GetComponent<Canvas>().enabled = true;
        Fade(true);
        StartCoroutine(FadeAndLoadSceneCoroutine(sceneIndex));
    }

    IEnumerator FadeAndLoadSceneCoroutine(int sceneIndex)
    {
        yield return new WaitForSeconds(FadeSpeed);

        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single).completed += (asyncOperation) => {
            Invoke("FadeOutDelayed", FadeDelayAfterLoad);
        };
    }

    void FadeOutDelayed()
    {
        Fade(false);
        Invoke("DisableCanvasDelayed", FadeSpeed);
    }

    void DisableCanvasDelayed()
    {
        SceneLoaderCanvas.GetComponent<Canvas>().enabled = false;
    }

    void Fade(bool fadeIn)
    {
        if (Faders == null || Faders.Length == 0)
            return;

        foreach (var fader in Faders)
        {
            fader.GetComponent<IFader>().Fade(fadeIn);
        }
    }

}
