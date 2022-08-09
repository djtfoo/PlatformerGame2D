/**
 * Created: 6 Aug 2022
 * 
 * Class: UI_GameTimer
 * Provides method to update a UI TMP text that represents the game timer.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class UI_GameTimer : MonoBehaviour
{
    private TMP_Text textObject;

    void Awake()
    {
        textObject = GetComponent<TMP_Text>();
    }

    public void UpdateTimerText()
    {
        // Compute seconds (s) and milliseconds (ms)
        int s = (int)GameStateManager.Instance.GameTimer;
        int ms = (int)(100f * (GameStateManager.Instance.GameTimer - s));

        // Update text
        textObject.text = string.Format("{0}:{1}", s, ms.ToString("00"));
    }
}
