using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CheckGridCreation : MonoBehaviour
{
    [SerializeField] private TMP_InputField rows;
    [SerializeField] private TMP_InputField columns;
    [SerializeField] private TMP_Text warningText;

    public static Action OnValidatedInputs;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        
        //InputController.OnValueChanged += CheckForEnableability;
        //InputController.OnValueChanged += ValidateInputs;
    }

    private void Start()
    {
        warningText.enabled = false;
    }

    public void ValidateInputs()
    {




        /*
        if(rows.text != "" && columns.text != "" && rows.text != "\0" && columns.text != "\0")
        {

            int aux = 0;

            if (int.TryParse(rows.text, out aux) && aux >= InputController.GetMinValueCount() && aux <= InputController.GetMaxValueCount())
            {
                if (int.TryParse(columns.text, out aux) && aux >= InputController.GetMinValueCount() && aux <= InputController.GetMaxValueCount())
                {
                    warningText.enabled = false;
                    OnValidatedInputs();
                    Debug.Log("Ta bien");
                }
                else
                {
                    warningText.enabled = true;
                    btn.interactable = false;
                    Debug.Log("Grid Creation else 1");
                }
            }
            else
            {
                warningText.enabled = true;
                btn.interactable = false;
                Debug.Log("Grid Creation else 2");
            }
        }
        else
        {
            warningText.enabled = true;
            btn.interactable = false;
            Debug.Log("Grid Creation else 3");
        }*/
    }

    private void CheckForEnableability()
    {
        /*
        if(rows.text != "" && columns.text != "" && rows.text != "\0" && columns.text != "\0")
        {
            int aux = 0;

            if (int.TryParse(rows.text, out aux) && aux >= InputController.GetMinValueCount() && aux <= InputController.GetMaxValueCount())
            {
                if (int.TryParse(columns.text, out aux) && aux >= InputController.GetMinValueCount() && aux <= InputController.GetMaxValueCount())
                {
                    warningText.enabled = false;
                    btn.interactable = true;
                }
                else
                {
                    warningText.enabled = true;
                    btn.interactable = false;
                }
            }
            else
            {
                warningText.enabled = true;
                btn.interactable = false;
            }
        }
        */
    }

    private void OnDestroy()
    {
        //InputController.OnValueChanged -= CheckForEnableability;
        //InputController.OnValueChanged -= ValidateInputs;
    }

}
