using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int totalScore = 0;
    private int miniGameScore = 0; // 미니게임 점수
    private int bestMiniGameScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 삭제되지 않음
            
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
        if(score> bestMiniGameScore)
            bestMiniGameScore = score;
    }

    public int GetMiniGameScore()
    {
        return bestMiniGameScore;
    }

    public void ApplyMiniGameScoreToTotal()
    {
        AddScore(miniGameScore);
        miniGameScore = 0;
    }
}
