using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;

    [SerializeField] private DialogBox dialogBox;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNotification(string[] textFields, TextButtonParameters[] buttonParameters)
    {
        dialogBox.SetActive(textFields, buttonParameters);
    }

    //public static void SetNotification(string[] textFields, IconButtonParameters[] buttonParameters)
    //{

    //}

    //public static void SetNotification(string[] textFields, IconTextButtonParameters[] buttonParameters)
    //{

    //}
}

public class TextButtonParameters
{
    public string text;
    public System.Action callback;
}

public class IconButtonParameters
{
    public Sprite sprite;
    public System.Action callback;
}

public class IconTextButtonParameters
{
    public string text;
    public Sprite sprite;
    public System.Action callback;
}
