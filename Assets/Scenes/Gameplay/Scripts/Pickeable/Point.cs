using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : PickUpAble
{
    public override void PickUp()
    {
        base.PickUp();
        Destroy(this.gameObject);

    }

}
