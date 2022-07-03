using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Character))]
public class StalkerControl : MonoBehaviour
{
    private Movement movement;

    [SerializeField] [Range(0.0f,0.5f)] private float movementTime;

    private bool moving;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void Start()
    {
        moving = false;
    }
    private void Update()
    {
        if(!moving)
        {
            StartCoroutine(GenerateMovement());
        }
    }

    private void MoveRandomly()
    {
        movement.SetNextMovement((MovementDirection)Random.Range(0,4));
    }

    private IEnumerator GenerateMovement()
    {
        float t = 0;
        moving = true;
        while (t < movementTime)
        {
            t += Time.deltaTime;
            if (t > 1) t = 1;
            yield return null;
        }
        MoveRandomly();
        moving = false;
    }



}
