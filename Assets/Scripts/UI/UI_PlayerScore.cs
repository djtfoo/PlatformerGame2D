using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PlayerScore : MonoBehaviour
{
    private TMP_Text textObject;

    void Awake()
    {
        textObject = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    public void UpdateScoreText(Player player)
    {
        // Update text
        textObject.text = string.Format("{0}", player.Score.ToString("000000"));
    }
}
