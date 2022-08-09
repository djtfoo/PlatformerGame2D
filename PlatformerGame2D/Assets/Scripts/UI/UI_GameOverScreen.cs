/**
 * Created: 7 Aug 2022
 * 
 * Class: UI_GameOverScreen
 * Allow other classes to trigger the game over screen object to transition to a Win or Lose state.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UI_GameOverScreen : MonoBehaviour
{
    [SerializeField] private string winTrigger = "Win";
    [SerializeField] private string loseTrigger = "Lose";

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWinState(bool won)
    {
        if (won)
            animator.SetTrigger(winTrigger);
        else
            animator.SetTrigger(loseTrigger);
    }
}
