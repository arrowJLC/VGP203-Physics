using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int PlayerScore = 0;
    public TMP_Text playerScore;

    private void Awake()
    {
  
        if (Instance == null)
        {
            if (playerScore)
                playerScore.text = PlayerScore.ToString();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}