using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameUIState
{
    Score
}

public class GameUIManager : MonoBehaviour
{
    public ScoreBoardUI scoreUI;
    private GameUIState currentState;

    public static GameUIManager Instance;


}
