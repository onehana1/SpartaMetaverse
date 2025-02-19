using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int totalScore = 0;
    private int miniGameScore = 0; // �̴ϰ��� ����

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �������� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log("�� ����: " + totalScore);
    }

    public void SetMiniGameScore(int score)
    {
        miniGameScore = score;
    }

    public int GetMiniGameScore()
    {
        return miniGameScore;
    }

    public void ApplyMiniGameScoreToTotal()
    {
        AddScore(miniGameScore);
        miniGameScore = 0;
    }
}
