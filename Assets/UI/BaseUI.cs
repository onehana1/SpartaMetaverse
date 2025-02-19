using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected MiniGameUIManager uiManager;

    public virtual void Init(MiniGameUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    protected abstract UIState GetUIState();
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}