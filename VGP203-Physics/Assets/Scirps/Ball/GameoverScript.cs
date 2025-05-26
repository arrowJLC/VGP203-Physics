using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameoverScript : MonoBehaviour
{
    public GameObject pausePanel;
    public bool gamePaused;
    [SerializeField] private int waitTime;

    public void ballStolen()
    {
        StartCoroutine(loadPauseGame());
    }

    public void pauseGame()
    {
        gamePaused = !gamePaused;
        pausePanel.SetActive(gamePaused);

        if (gamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        StartCoroutine(loadGameOver());
    }

    IEnumerator loadGameOver()
    {
        Debug.Log("Wait time started");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator loadPauseGame()
    {
        yield return new WaitForSeconds(2);
        pauseGame();
    }
}

