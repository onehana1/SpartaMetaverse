using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int totalScore = 0;
    private int bestMiniGameScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log("ÃÑ Á¡¼ö: " + totalScore);
    }

    public void SetMiniGameScore(int score)
    {
        if (score > bestMiniGameScore)
            bestMiniGameScore = score;
    }

    public int GetMiniGameScore()
    {
        return bestMiniGameScore;
    }
}
