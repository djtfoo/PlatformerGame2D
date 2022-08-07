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

    // Update is called once per frame
    public void UpdateLivesText(Player player)
    {
        // Update text
        textObject.text = string.Format("X{0}", player.NumLives);
    }
}
