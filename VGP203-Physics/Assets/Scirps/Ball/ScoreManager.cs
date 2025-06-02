//////using TMPro;
//////using UnityEngine;

//////public class ScoreManager : MonoBehaviour
//////{
//////    public static ScoreManager Instance;
//////    public int PlayerScore = 0;
//////    public TMP_Text playerScore;

//////    private void Awake()
//////    {

//////        if (Instance == null)
//////        {
//////            if (playerScore)
//////                playerScore.tmp = PlayerScore.ToString();
//////            Instance = this;
//////            DontDestroyOnLoad(gameObject);
//////        }
//////        else
//////        {
//////            Destroy(gameObject);
//////        }
//////    }
//////}

////using TMPro;
////using UnityEngine;

////public class ScoreManager : MonoBehaviour
////{
////    public static ScoreManager Instance;
////    public int PlayerScore = 0;
////    public TMP_Text playerScore;

////    private void Awake()
////    {
////        // Singleton pattern to persist ScoreManager
////        if (Instance == null)
////        {
////            Instance = this;
////            DontDestroyOnLoad(gameObject);
////        }
////        else
////        {
////            Destroy(gameObject);
////            return;
////        }

////        UpdateScoreUI();
////    }

////    // Call this method whenever the player score changes
////    public void AddScore(int amount)
////    {
////        PlayerScore += amount;
////        UpdateScoreUI();
////    }

////    // Updates the TMP_Text with the current score
////    private void UpdateScoreUI()
////    {
////        if (playerScore != null)
////        {
////            playerScore.text = PlayerScore.ToString();
////        }
////    }
////}







//using TMPro;
//using UnityEngine;

//public class ScoreManager : MonoBehaviour
//{
//    public static ScoreManager Instance;
//    public int PlayerScore = 0;
//    public TMP_Text playerScoreText;

//    private void Awake()
//    {
//        // Singleton pattern
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(this.gameObject); // Make parent persistent
//        }
//        else
//        {
//            Destroy(gameObject);
//            return;
//        }

//        UpdateScoreUI();
//    }

//    private void Start()
//    {
//        // Redundantly update in case Awake missed it
//        UpdateScoreUI();
//    }

//    public void AddScore(int amount)
//    {
//        PlayerScore += amount;
//        UpdateScoreUI();
//    }

//    public void SetScoreText(TMP_Text newText)
//    {
//        playerScoreText = newText;
//        UpdateScoreUI();
//    }

//    private void UpdateScoreUI()
//    {
//        if (playerScoreText != null)
//        {
//            playerScoreText.text = " " + PlayerScore.ToString();
//            //ScoreManager.Instance.SetScoreText(playerScore);
//        }
//        else
//        {
//            Debug.LogWarning("Score Text is not assigned!");
//        }
//    }
//}





using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int PlayerScore = 0;
    public TMP_Text playerScoreText;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        updateScore();
    }

    private void Start()
    {
        updateScore(); 
    }

    public void addScore(int amount)
    {
        PlayerScore += amount;
        updateScore();
    }

    public void scoreText(TMP_Text newText)
    {
        playerScoreText = newText;
        updateScore();
    }

    private void updateScore()
    {
        if (playerScoreText != null)
        {
            playerScoreText.text = $"{PlayerScore}";
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned!");
        }
    }
}
