using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int totalScore = 0;
    private int bestMiniGameScore = 0;
    private int caughtGiftCount = 0;

    private const string BestScoreKey = "BestMiniGameScore";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadBestScore();

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log("총 점수: " + totalScore);
    }

    public void SetMiniGameScore(int score)
    {
        if (score > bestMiniGameScore)
        {
            bestMiniGameScore = score;
            PlayerPrefs.SetInt(BestScoreKey, bestMiniGameScore); // BestScoreKeyㅇㅔ 저장
            PlayerPrefs.Save();
        }
    }

    public int GetMiniGameScore()
    {
        return bestMiniGameScore;
    }

    public void SetCaughtGiftCount(int count)
    {
        caughtGiftCount = count;
    }
    public int GetCaughtGiftCount()
    {
        return caughtGiftCount;
    }
    private void LoadBestScore()
    {
        bestMiniGameScore = PlayerPrefs.GetInt(BestScoreKey, 0); // 없으면 0
    }
}
