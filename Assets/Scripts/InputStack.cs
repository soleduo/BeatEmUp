using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStack
{ 
    Stack<int> buttonStack = new Stack<int>();
    // Start is called before the first frame update

    public event System.Action<int> OnInputConsumed;
    public void Add(int button)
    {
        buttonStack.Push(button);
    }

    public void Consume()
    {
        if (buttonStack.Count <= 0)
            return;

        int i = buttonStack.Pop();
        buttonStack.Clear();
        OnInputConsumed.Invoke(i);
    }
}

public class InputButton
{

}
