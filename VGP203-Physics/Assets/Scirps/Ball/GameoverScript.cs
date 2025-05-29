//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System.Collections;


//public class GameoverScript : MonoBehaviour
//{
//    public GameObject pausePanel;
//    public bool gamePaused;
//    [SerializeField] private int waitTime;

//    public void ballStolen()
//    {
//        StartCoroutine(loadPauseGame());
//    }

//    public void pauseGame()
//    {
//        gamePaused = !gamePaused;
//        pausePanel.SetActive(gamePaused);

//        if (gamePaused)
//            Time.timeScale = 0;
//        else
//            Time.timeScale = 1;
//        StartCoroutine(loadGameOver());
//    }

//    IEnumerator loadGameOver()
//    {
//        Debug.Log("Wait time started");
//        yield return new WaitForSeconds(waitTime);
//        SceneManager.LoadScene("GameOver");
//    }

//    IEnumerator loadPauseGame()
//    {
//        yield return new WaitForSeconds(2);
//        pauseGame();
//    }
//}




using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameoverScript : MonoBehaviour
{
    public GameObject pausePanel;
    public bool gamePaused;
    [SerializeField] private int waitTime = 10;
    bool canReset = true;

    public void ballStolen()
    {
        StartCoroutine(LoadPauseGame());
    }

    public void pauseGame()
    {
        gamePaused = !gamePaused;

        //if (pausePanel != null)
        //{
            pausePanel.SetActive(gamePaused);
        //}

        Time.timeScale = gamePaused ? 0 : 1;

        //FindAnyObjectByType<GameoverScript>().ResetGame();
        ResetGame();
    }

    IEnumerator LoadPauseGame()
    {
        yield return new WaitForSeconds(waitTime);
        pauseGame();
    }

    public void ResetGame()
    {
        int playerScore = ScoreManager.Instance.PlayerScore;

        if (canReset)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (playerScore >= 7)
        {
            canReset = false;
            Time.timeScale = 1; // Make sure game is unpaused before switching scenes
            SceneManager.LoadScene("GameOver");
            return;
        }
       
        
        
    }
}

