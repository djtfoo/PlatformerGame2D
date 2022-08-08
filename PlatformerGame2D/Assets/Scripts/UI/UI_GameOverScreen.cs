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
