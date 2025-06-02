////using UnityEngine;
////using UnityEngine.SceneManagement;

////public class WInScript : MonoBehaviour
////{
////    public GameObject playerGoal;

////    private void OnTriggerEnter2D(Collider2D collision)
////    {
////        if (collision.CompareTag("Ball"))
////        {
////            playerGoal.SetActive(true);
////            SceneManager.LoadScene("LevelTwo");
////        }
////    }
////}

//using UnityEngine;
//using UnityEngine.SceneManagement;
//using TMPro;

//public class WinScript : MonoBehaviour
//{
//    [Header("UI/Effects")]
//    public GameObject playerGoal;
//    public TMP_Text playerScore;

//    private int pScore = 0;
//    private Vector3 initialPosition;
//    private Quaternion initialRotation;

//    //[Header("Scene Settings")]
//    //public string nextSceneName = "Level Two";  // Set this from the Inspector

//    private void Start()
//    {
//        // Save initial state
//        initialPosition = transform.position;
//        initialRotation = transform.rotation;
//    }

//    public void ResetObject()
//    {
//        // Reset to initial state
//        transform.position = initialPosition;
//        transform.rotation = initialRotation;
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Ball"))
//        {
//            Debug.Log("Ball entered goal. Winning...");

//            // Optional: Show goal UI or effects
//            if (playerGoal != null)
//                playerGoal.SetActive(true);

//            ScoreManager.Instance.AddScore(1);

//            playerScore.text = "Score: " + ScoreManager.Instance.PlayerScore;

//            // Load the next scene
//            //SceneManager.LoadScene(nextSceneName);

//        }
//    }

//    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//    //{
//    //    if (scene.name == nextSceneName)
//    //    {
//    //        if (scene.name == nextSceneName)
//    //        {
//    //            Debug.Log("Next level loaded: " + scene.name);

//    //            // Re-find the playerScore UI text in the new scene
//    //            playerScore = GameObject.FindWithTag("PlayerScore")?.GetComponent<TMP_Text>();

//    //            if (playerScore != null)
//    //            {
//    //                playerScore.text = "Score: " + pScore.ToString();
//    //                //ScoreManager.Instance.AddScore(points)
//    //            }
//    //            else
//    //            {
//    //                Debug.LogWarning("playerScore text not found in new scene.");
//    //            }
//    //        }
//    //        // Optional: Call game-over or win logic here
//    //        // GameOver(); 
//    //    }
//    //}

//    //private void OnDestroy()
//    //{
//    //    // Clean up event listener
//    //    SceneManager.sceneLoaded -= OnSceneLoaded;
//    //}
//}


using UnityEngine;
using TMPro;

public class WinScript : MonoBehaviour
{
    [Header("UI/Effects")]
    public GameObject playerGoal;
    public TMP_Text playerScore;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool hasScored = false;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        if (playerScore != null)
        {
            ScoreManager.Instance.scoreText(playerScore);
        }
    }

    public void ResetObject()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        hasScored = false;

        if (playerGoal != null)
        {
            playerGoal.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasScored) return;

        if (collision.CompareTag("Ball"))
        {
            hasScored = true;

            Debug.Log("Ball entered goal");

            if (playerGoal != null)
                playerGoal.SetActive(true);

            ScoreManager.Instance.addScore(1);
        }
    }
}
