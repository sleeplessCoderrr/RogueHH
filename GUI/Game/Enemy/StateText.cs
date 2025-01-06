﻿using UnityEngine;
using UnityEngine.UI;

public class StateText : MonoBehaviour
{
    public Text indicatorText;

    private void OnEnable()
    {
        indicatorText.text = "";
    }

    public void UpdateIndicator(EnemyState state)
    {
        if (indicatorText == null)
        {
            Debug.LogError("Indicator Text is not assigned.");
            return;
        }
        
        switch (state)
        {
            case EnemyState.Alert:
                indicatorText.text = "??";
                indicatorText.color = Color.yellow;
                break;
            case EnemyState.Aggro:
                indicatorText.text = "!!!";
                indicatorText.color = Color.red;
                break;
            case EnemyState.Idle:
                indicatorText.text = "";
                break;
            case EnemyState.Attack:
                indicatorText.text = "";
                break;
        }
    }
}