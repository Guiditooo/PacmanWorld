using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUpAble : MonoBehaviour
{
    private static int count;

    public static Action<int> OnPickUp;
    public static Action OnAllPickedUp;

    [SerializeField] private int pointsToGive;
    private void Awake()
    {
        count = 0;
    }
    private void Start()
    {
        count++;
        //Debug.Log(this.gameObject.name + " Count: " + count);
    }

    public virtual void PickUp()
    {
        OnPickUp?.Invoke(pointsToGive);
    }

    private void OnDestroy()
    {
        count--;
        //Debug.Log(this.gameObject.name + " Count: " + count);
        
        if(count == 0)
        {
            OnAllPickedUp?.Invoke();
        }
    }


}
