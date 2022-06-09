using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private int maxCharCount = 2;
    [SerializeField] static private int minValueCount = 10;
    [SerializeField] static private int maxValueCount = 100;

    public static int GetMinValueCount() => minValueCount;
    public static int GetMaxValueCount() => maxValueCount;

    private TMPro.TMP_InputField inputField;

    public static Action OnValueChanged;
    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValidateInput += ValidateInput;

        //inputField.onValueChanged.AddListener(ClampValue);

    }

    private void ClampValue(string value)
    {
        int aux = 0;

        if(int.TryParse(value,out aux))
        {
            if (aux <= minValueCount)
            {
                inputField.text = minValueCount.ToString();
            }
            else if(aux >= maxValueCount)
            {
                inputField.text = maxValueCount.ToString();
            }
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        if (charIndex > maxCharCount) return '\0';

        int next = 0;

        if (charIndex == 0 && addedChar == '0')
        {
            return '\0';
        }
        else
        {
            if (int.TryParse(addedChar.ToString(), out next))
            {
                if(int.TryParse(text+addedChar.ToString(), out next))
                {
                    if(next <= maxValueCount)
                    {
                        OnValueChanged();
                        return addedChar;
                    }
                    else
                    {
                        inputField.text = maxValueCount.ToString();
                        OnValueChanged();
                        return '\0';
                    }
                }
            }
        }

        return '\0';
    }



    private void OnDestroy()
    {
        inputField.onValidateInput -= ValidateInput;
        inputField.onValueChanged.RemoveListener(ClampValue);
    }
}
