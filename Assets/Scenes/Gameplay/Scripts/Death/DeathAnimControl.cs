using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimControl : MonoBehaviour
{
    public System.Action OnDeathAnimationEnd;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void EndAnimation()
    {
        OnDeathAnimationEnd?.Invoke();
    }

    public void StartAnimation()
    {
        animator.SetTrigger("OnDeath");
    }
}
