using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameoverUI;

    public void EndGame()
    {
        gameoverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
