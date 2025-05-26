//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class WInScript : MonoBehaviour
//{
//    public GameObject playerGoal;

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Ball"))
//        {
//            playerGoal.SetActive(true);
//            SceneManager.LoadScene("LevelTwo");
//        }
//    }
//}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScript : MonoBehaviour
{
    [Header("UI/Effects")]
    public GameObject playerGoal;
    public TMP_Text playerScore;
    
    private int pScore = 0;

    [Header("Scene Settings")]
    public string nextSceneName = "Level Two";  // Set this from the Inspector

    private void Start()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Debug.Log("Ball entered goal. Winning...");

            // Optional: Show goal UI or effects
            if (playerGoal != null)
                playerGoal.SetActive(true);

            ScoreManager.Instance.PlayerScore += 1;
            playerScore.text = "Score: " + ScoreManager.Instance.PlayerScore;

            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == nextSceneName)
        {
            if (scene.name == nextSceneName)
            {
                Debug.Log("Next level loaded: " + scene.name);

                // Re-find the playerScore UI text in the new scene
                playerScore = GameObject.FindWithTag("PlayerScore")?.GetComponent<TMP_Text>();

                if (playerScore != null)
                {
                    playerScore.text = "Score: " + pScore.ToString();
                }
                else
                {
                    Debug.LogWarning("playerScore text not found in new scene.");
                }
            }
            // Optional: Call game-over or win logic here
            // GameOver(); 
        }
    }

    private void OnDestroy()
    {
        // Clean up event listener
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
