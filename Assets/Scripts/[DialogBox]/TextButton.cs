using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
    [SerializeField]private Button button;
    [SerializeField]private Text text;

    public void SetButton(TextButtonParameters parameters)
    {
        string _text = parameters.text;
        System.Action callback = parameters.callback;

        this.text.text = _text;
        button.onClick.AddListener(() => { callback.Invoke(); button.enabled = false; });

        button.enabled = true;
        SetActive(true);
    }

    public void SetActive(bool isActive)
    {
        if (gameObject.activeSelf == isActive)
            return;

        gameObject.SetActive(isActive);

    }
}
