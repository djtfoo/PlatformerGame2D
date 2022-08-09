/**
 * Created: 6 Aug 2022
 * 
 * Class: UI_PlayerScore
 * Provides method to update a UI TMP text that represents the player's score.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class UI_PlayerScore : MonoBehaviour
{
    private TMP_Text textObject;

    void Awake()
    {
        textObject = GetComponent<TMP_Text>();
    }

    public void UpdateScoreText(PlayerState player)
    {
        // Update text
        textObject.text = string.Format("{0}", player.Score.ToString("000000"));
    }
}
