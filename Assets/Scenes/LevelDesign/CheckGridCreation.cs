using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CheckGridCreation : MonoBehaviour
{
    [SerializeField] private TMP_InputField rows;
    [SerializeField] private TMP_InputField columns;
    [SerializeField] private TMP_Text warningText;



    private void Start()
    {
        warningText.enabled = false;
    }

    

}
