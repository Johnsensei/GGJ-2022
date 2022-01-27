using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
