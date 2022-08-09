/**
 * Created: 7 Aug 2022
 * 
 * Class: WinGameObject
 * Represents an Object that can handle or check for a win condition in the game.
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WinGameObject : Object
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
    }

    protected abstract void CheckWinCondition();
}
