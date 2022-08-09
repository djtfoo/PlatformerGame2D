/**
 * Created: 7 Aug 2022
 * 
 * Class: UI_PlayerLives
 * Provides method to update a UI TMP text that represents the number of player lives left.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class UI_PlayerLives : MonoBehaviour
{
    private TMP_Text textObject;

    void Awake()
    {
        textObject = GetComponent<TMP_Text>();
    }

    public void UpdateLivesText(PlayerState player)
    {
        // Update text
        textObject.text = string.Format("X{0}", player.NumLives);
    }
}
