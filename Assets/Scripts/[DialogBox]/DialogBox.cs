using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private Text[] fields;
    [SerializeField] private TextButton[] buttons;
    
    public void SetActive(string[] texts, TextButtonParameters[] buttonParameters)
    {

    }

    private void SetTexts(string[] texts)
    {

    }

    private void SetButtons(TextButtonParameters[] buttonParameters)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if(buttonParameters.Length < i)
            {
                buttons[i].SetActive(false);
                continue;
            }

            buttons[i].SetButton(buttonParameters[i]);
        }
    }
}
