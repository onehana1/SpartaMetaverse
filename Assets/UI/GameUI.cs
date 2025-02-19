using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI time;


    private void Start()
    {

    }



    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}