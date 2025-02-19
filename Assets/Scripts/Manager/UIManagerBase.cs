using UnityEngine;


public enum UIState
{
    Home,
    Game,
    GameOver,
    Score
}

public abstract class UIManagerBase : MonoBehaviour
{
   // public abstract void ChangeState(UIState state);
}