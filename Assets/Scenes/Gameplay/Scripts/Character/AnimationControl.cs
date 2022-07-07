using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator animator;

    private Movement movement;

    private SpriteRenderer sr;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();
        movement.OnMove += ChangeMovementDirection;
    }
    private void OnDestroy()
    {
        movement.OnMove -= ChangeMovementDirection;
    }

    private void ChangeMovementDirection(MovementDirection direction)
    {
        animator.SetBool("Walking", direction != MovementDirection.None);
        if(direction==MovementDirection.Right)
        {
            sr.flipX = true;
        }
        if (direction == MovementDirection.Left)
        {
            sr.flipX = false;
        }

    }


}
