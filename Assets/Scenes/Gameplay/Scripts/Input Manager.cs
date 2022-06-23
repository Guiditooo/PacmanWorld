using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InputManager : MonoBehaviour
{
    public static Action PressingLeft; 
    public static Action PressingRight; 
    public static Action PressingUp; 
    public static Action PressingDown; 
    public static Action PressingEscape;

    private void Update()
    {

        if (Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S)) PressingDown();
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) PressingLeft();
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) PressingRight();
        
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) PressingUp();
        
        if (Input.GetKey(KeyCode.Escape)) PressingEscape();
    }

}
