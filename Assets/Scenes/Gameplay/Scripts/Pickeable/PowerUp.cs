using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : PickUpAble
{
    public override void PickUp()
    {
        base.PickUp();

        //TO DO Darle poder al player

        Destroy(this.gameObject);
    }

}
